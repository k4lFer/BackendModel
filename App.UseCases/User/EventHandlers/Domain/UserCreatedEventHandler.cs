using App.Domain.User.Events;
using App.Interfaces.Ports.Emails;
using App.Interfaces.Ports.Emails.Models;
using App.Shared.Security;
using Cortex.Mediator.Notifications;
using System.Security.Claims;

namespace App.UseCases.User.EventHandlers.Domain;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ITemplateRenderer _templateRenderer;
    private readonly ITokenProvider _tokenProvider;

    public UserCreatedEventHandler(
        IEmailSender emailSender, 
        ITemplateRenderer templateRenderer,
        ITokenProvider tokenProvider)
    {
        _emailSender = emailSender;
        _templateRenderer = templateRenderer;
        _tokenProvider = tokenProvider;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, notification.Username),
            new Claim(ClaimTypes.Email, notification.Email)
        };
        
        var token = _tokenProvider.GenerateToken(
            notification.UserId.ToString(), 
            claims, 
            App.Shared.Objects.Enums.TokenType.Access
        );

        var htmlBody = await _templateRenderer.RenderAsync("WelcomeEmail.cshtml", new 
        { 
            Username = notification.Username,
            Token = token
        });

        var message = new EmailMessage(
            To: notification.Email,
            Subject: "¡Bienvenido a nuestra plataforma!",
            BodyHtml: htmlBody
        );

        await _emailSender.SendAsync(message, cancellationToken);
    }
}
