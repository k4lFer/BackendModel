using App.Domain.User.Events;
using App.Interfaces.Ports.Emails;
using App.Interfaces.Ports.Emails.Models;
using App.Shared.Objects.Enums;
using App.Shared.Security;
using Cortex.Mediator.Notifications;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace App.UseCases.User.EventHandlers.Domain;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ITemplateRenderer _templateRenderer;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(
        IEmailSender emailSender, 
        ITemplateRenderer templateRenderer,
        ITokenProvider tokenProvider,
        ILogger<UserCreatedEventHandler> logger)
    {
        _emailSender = emailSender;
        _templateRenderer = templateRenderer;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, notification.Email),
            new Claim(ClaimTypes.NameIdentifier, notification.IsEmailConfirmed.ToString()),
        };
        
        var token = _tokenProvider.GenerateToken(
            notification.UserId.ToString(), 
            claims, 
            TokenType.EmailConfirmation
        );

        var htmlBody = await _templateRenderer.RenderAsync("WelcomeEmail.html", new 
        { 
            Username = notification.Username,
            Token = token
        });

        var message = new EmailMessage(
            To: notification.Email,
            Subject: "¡Bienvenido a nuestra plataforma!",
            BodyHtml: htmlBody
        );

        _logger.LogInformation("Sending welcome email to {Email}", notification.Email);
        await _emailSender.SendAsync(message, cancellationToken);
    }
}
