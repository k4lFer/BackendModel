using System.Security.Claims;
using App.Domain.User.Entities;
using App.Interfaces.Ports;
using App.Interfaces.Ports.User;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Objects.Enums;
using App.Shared.Result;
using App.Shared.Security;
using Cortex.Mediator.Commands;

namespace App.UseCases.Auth.Command.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, OutputPort<LoginResponseDto>>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenHasher _tokenHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        ITokenHasher tokenHasher,
        IUnitOfWork unitOfWork)
    {
        _tokenProvider = tokenProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _tokenHasher = tokenHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputPort<LoginResponseDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var rawToken = command.Dto.RefreshToken;
        var tokenHash = _tokenHasher.Hash(rawToken);

        var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);

        if (storedToken is null || !storedToken.IsActive)
        {
            return OutputPort<LoginResponseDto>.Failure(
                System.Net.HttpStatusCode.Unauthorized,
                new MessageDto(code: "INVALID_REFRESH_TOKEN", message: "The refresh token is invalid or has been revoked.")
            );
        }

        var user = await _userRepository.GetByIdAsync(storedToken.UserId, cancellationToken);
        if (user is null)
        {
            return OutputPort<LoginResponseDto>.Failure(
                System.Net.HttpStatusCode.Unauthorized,
                new MessageDto(code: "USER_NOT_FOUND", message: "User not found.")
            );
        }

        storedToken.Revoke();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var newAccessToken = _tokenProvider.GenerateToken(user.Id.ToString(), claims, TokenType.Access);
        var newRefreshToken = _tokenProvider.GenerateToken(user.Id.ToString(), claims, TokenType.Refresh);
        var newRefreshTokenHash = _tokenHasher.Hash(newRefreshToken);

        var newRefreshTokenEntity = TRefreshToken.Create(
            user.Id,
            storedToken.DeviceId,
            newRefreshTokenHash,
            expiresAt: null,
            storedToken.IpAddress,
            storedToken.UserAgent
        );

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        var response = new LoginResponseDto
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            UserId = user.Id,
            Email = user.Email,
            Username = user.Username
        };

        return OutputPort<LoginResponseDto>.Success(response, System.Net.HttpStatusCode.OK, "Token refreshed successfully");
    }
}