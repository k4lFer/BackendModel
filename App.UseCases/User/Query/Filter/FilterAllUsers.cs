using App.Objects.Common;
using App.Objects.User.DTOs.Output.Response;

namespace App.UseCases.User.Query.Filter;

public class FilterAllUsers : QueryFilter<UsersResponseDto>
{
    public string? Email  { get; set; }
    
    public override IQueryable<UsersResponseDto> ApplyFilter(IQueryable<UsersResponseDto> query)
    {
        if (!string.IsNullOrWhiteSpace(Email))
            query = query.Where(u => u.Email!.Contains(Email));

        return query;
    }
}