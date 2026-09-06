using System.Net;
using App.Interfaces.Ports;
using App.Interfaces.Ports.User;
using App.Shared.Result;
using Cortex.Mediator.Commands;

namespace App.UseCases.Auth.Command.Logout;

public class LogoutCommandHandler : ICommandHandler<LogoutCommand, OutputPort<object>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutputPort<object>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.DeviceId))
        {
            return OutputPort<object>.Failure(
                System.Net.HttpStatusCode.BadRequest,
                new MessageDto(code: "DEVICE_ID_REQUIRED", message: "The X-Device-Id header is required to log out.")
            );
        }

        await _refreshTokenRepository.RevokeTokensByDeviceAsync(command.UserId, command.DeviceId, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return OutputPort<object>.Success(
            data: null,
            statusCode: HttpStatusCode.OK,
            message: "Logged out successfully"
        );
    }
}
