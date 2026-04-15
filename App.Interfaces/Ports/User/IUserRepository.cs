using App.Domain.User.Entities;
using App.Objects.Common;
using App.Objects.User.DTOs.Output.Response;

namespace App.Interfaces.Ports.User;

public interface IUserRepository : IBaseRepository<TUser>
{
    Task<QueryResult<UsersResponseDto?>> GetUsersPaged(int page, int pageSize, QueryFilter<UsersResponseDto>? filter = null);
}