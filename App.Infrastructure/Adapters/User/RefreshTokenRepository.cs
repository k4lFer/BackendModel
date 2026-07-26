using App.Domain.User.Entities;
using App.Infrastructure.Core.DataBaseContext.Connection;
using App.Interfaces.Ports.User;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Adapters.User;

public class RefreshTokenRepository : BaseRepository<TRefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDataBaseContext dbc) : base(dbc) { }

    public async Task<TRefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        return await _dbc.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
    }

    public async Task RevokeAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var activeTokens = await _dbc.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
            .ToListAsync(cancellationToken);

        foreach (var token in activeTokens)
        {
            token.Revoke();
        }
    }

    public async Task RevokeTokenAsync(Guid tokenId, CancellationToken cancellationToken = default)
    {
        var token = await _dbc.RefreshTokens.FindAsync(new object[] { tokenId }, cancellationToken);
        if (token is not null)
        {
            token.Revoke();
        }
    }
}