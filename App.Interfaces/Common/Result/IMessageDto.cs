using App.Objects.Common.Objects;

namespace App.Interfaces.Common.Result;

public interface IMessageDto
{
    public IEnumerable<MessageDto?> Messages { get; set; }
}