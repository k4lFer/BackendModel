using App.Infrastructure.Adapters.Emails;
using App.Infrastructure.Adapters.Templates;
using App.Infrastructure.Core.Services.Security;
using App.Interfaces.Ports.Emails;
using App.Shared.Security;
using Cortex.Mediator.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Core.Services;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenHasher, TokenHasher>();
        services.AddSingleton<ITemplateRenderer, HtmlTemplateRenderer>();
        services.AddScoped<IEmailSender, MailKitEmailSender>();
        services.AddScoped<IEventBus, EventBus>();

        return services;
    }
}