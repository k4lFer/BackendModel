using App.Interfaces.Common.Result;
using App.Objects.Common.Objects;

namespace App.Interfaces.Common;

public interface IInputValidator<in T> : IHttpResponse
{
    IReadOnlyCollection<MessageDto> Messages { get; }
    public Task<bool> ValidateAsync(T input, CancellationToken cancellationToken = default);
}