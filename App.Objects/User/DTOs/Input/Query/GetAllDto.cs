using App.Objects.Common.Objects;

namespace App.Objects.User.DTOs.Input.Query;

public class GetAllDto : QueryDto
{
    public string? Email { get; }
}