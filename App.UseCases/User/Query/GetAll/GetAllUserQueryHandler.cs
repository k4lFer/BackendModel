using System.Net;
using App.Interfaces.Common.Result;
using App.Interfaces.Ports.User;
using App.Objects.Common;
using App.Objects.User.DTOs.Output.Response;
using App.UseCases.User.Query.Filter;
using Cortex.Mediator.Queries;

namespace App.UseCases.User.Query.GetAll;

public class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, OutputPort<QueryResult<UsersResponseDto>>>
{
    private readonly IUserRepository _userRepository;
    
    public GetAllUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<OutputPort<QueryResult<UsersResponseDto>>> Handle(GetAllUserQuery query, CancellationToken cancellationToken)
    {
        FilterAllUsers filter = new()
        {
            Email = query.Input.Email
        };
        
        var results = await _userRepository.GetUsersPaged(query.Input.NumberPage, query.Input.PageSize, filter);
        
        if (!results.Results.Any()) return OutputPort<QueryResult<UsersResponseDto>>.Success(data:null, statusCode: HttpStatusCode.NoContent);

        return OutputPort<QueryResult<UsersResponseDto>>.Success(data: results);
    }
}