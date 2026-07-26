using App.Domain.User.Entities;

namespace App.Interfaces.Ports.User;

public interface IRefreshTokenRepository : IBaseRepository<TRefreshToken>
{
    Task<TRefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);
    Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);
    Task RevokeTokenAsync(Guid tokenId, CancellationToken cancellationToken = default);
}