using App.Infrastructure;
using App.Infrastructure.Core.DataBaseContext.Connection;
using App.UseCases;
using WebApi.Config;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Scalar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSecurityConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddScalarConfiguration();
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddUseCasesDi();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDataBaseContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();