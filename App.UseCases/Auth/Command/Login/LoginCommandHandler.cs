using App.Domain.User.Entities;
using App.Interfaces.Ports.User;
using App.Objects.User.DTOs.Input.Command;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Objects.Enums;
using App.Shared.Result;
using App.Shared.Security;
using App.Shared.Validation;
using Cortex.Mediator.Commands;
using System.Security.Claims;

namespace App.UseCases.Auth.Command.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, OutputPort<LoginResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IInputValidator<LoginDto> _validator;
    
    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IInputValidator<LoginDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _validator = validator;
    }
    
    public async Task<OutputPort<LoginResponseDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        if (!await _validator.ValidateAsync(command.Input, cancellationToken))
        {
            return OutputPort<LoginResponseDto>.Failure(_validator.StatusCode, _validator.Messages.ToArray());
        }
        
        var dto = command.Input;
        
        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken);
        
        if (user is null || !_passwordHasher.VerifyHashedPassword(user.Password, dto.Password))
        {
            return OutputPort<LoginResponseDto>.Failure(
                System.Net.HttpStatusCode.Unauthorized,
                new MessageDto(code: "INVALID_CREDENTIALS", message: "Invalid email or password.")
            );
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        var token = _tokenProvider.GenerateToken(user.Id.ToString(), claims, TokenType.Access);
        
        var response = new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            Username = user.Username
        };
        
        return OutputPort<LoginResponseDto>.Success(response, System.Net.HttpStatusCode.OK, "Login successful");
    }
}
