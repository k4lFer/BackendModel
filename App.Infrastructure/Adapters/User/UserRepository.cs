using App.Domain.User.Entities;
using App.Infrastructure.Core.DataBaseContext.Connection;
using App.Interfaces.Ports.User;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Query;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Adapters.User;

public class UserRepository(AppDataBaseContext dbc) : BaseRepository<TUser>(dbc), IUserRepository
{
    public async Task<TUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbc.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    
    public async Task<QueryResult<UsersResponseDto?>> GetUsersPaged(int page, int pageSize, QueryFilter<UsersResponseDto>? filter = null)
    {
        IQueryable<UsersResponseDto> query =
            _dbc.Users
                .AsNoTracking()
                .Select(u => new UsersResponseDto(
                    u.Id,
                    u.Email,
                    u.CreatedAt
                ));

        if (filter is not null) query = filter.ApplyFilter(query);

        return await PaginateAsync(query, page, pageSize);
    }
}