using App.Infrastructure.Adapters.Emails;
using App.Infrastructure.Adapters.Templates;
using App.Infrastructure.Core.Services.Security;
using App.Interfaces.Ports.Emails;
using App.Shared.Security;
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
        services.AddSingleton<ITemplateRenderer, RazorTemplateRenderer>();
        services.AddScoped<IEmailSender, MailKitEmailSender>();
        
        return services;
    }
}