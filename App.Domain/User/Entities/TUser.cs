using App.Objects.Common.Objects;

namespace App.Domain.User.Entities;

public class TUser : BaseDomain
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private TUser(string email, string username, string password,  DateTime createdAt, DateTime? updatedAt)
    {
        Email = email;
        Username = username;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static TUser Create(string email, string username, string password)
    {
        return new TUser(email, username, password,  DateTime.UtcNow, null);
    }
}