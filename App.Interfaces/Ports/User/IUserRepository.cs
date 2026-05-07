using App.Domain.User.Entities;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Query;

namespace App.Interfaces.Ports.User;

public interface IUserRepository : IBaseRepository<TUser>
{
    Task<TUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<QueryResult<UsersResponseDto?>> GetUsersPaged(int page, int pageSize, QueryFilter<UsersResponseDto>? filter = null);
}