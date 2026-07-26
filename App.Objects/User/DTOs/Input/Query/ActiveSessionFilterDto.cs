using App.Shared.Query;

namespace App.Objects.User.DTOs.Input.Query;

public class ActiveSessionFilterDto : QueryDto
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? DeviceId { get; set; }
}