using System.Net;
using App.Objects.User.DTOs.Input.Command;
using App.Shared.Result;
using App.Shared.Validation;

namespace App.UseCases.User.Command.CreateUser;

public class CreateUserCommandValidator : IInputValidator<CreateUserDto>
{
    private readonly List<MessageDto> _messages = [];
    public HttpStatusCode StatusCode { get; private set; }
    public IReadOnlyCollection<MessageDto> Messages => _messages;
    
    public async Task<bool> ValidateAsync(CreateUserDto input, CancellationToken cancellationToken = default)
    {
        try
        {
            _messages.Clear();
            
            if (input is null)
            {
                _messages.Add(new MessageDto(code: "NULL_INPUT", message: "Input data is required."));

                StatusCode = HttpStatusCode.BadRequest;
                return false;
            }
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                _messages.Add(new MessageDto(code: "EMAIL_REQUIRED", message: "Email is required."));
            }

            if (string.IsNullOrEmpty(input.Username))
            {
                _messages.Add(new MessageDto(code: "USERNAME_REQUIRED", message: "Username is required."));
            }
            if (string.IsNullOrWhiteSpace(input.Password))
            {
                _messages.Add(new MessageDto(code: "PASSWORD_REQUIRED", message: "Password is required."));
            }
            
            if (_messages.Any())
            {
                StatusCode = HttpStatusCode.UnprocessableEntity; 
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _messages.Clear();
            _messages.Add(new MessageDto(code: "VALIDATION_EXCEPTION", message: ex.Message));
            StatusCode = HttpStatusCode.BadRequest;
            return false;
        }
    }
}