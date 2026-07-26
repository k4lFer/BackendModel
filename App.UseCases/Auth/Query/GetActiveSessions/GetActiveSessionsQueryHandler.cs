using System.Net;
using App.Interfaces.Ports.User;
using App.Objects.User.DTOs.Output.Response;
using App.Shared.Query;
using App.Shared.Result;
using App.UseCases.Auth.Filter;
using Cortex.Mediator.Queries;

namespace App.UseCases.Auth.Query.GetActiveSessions;

public class GetActiveSessionsQueryHandler : IQueryHandler<GetActiveSessionsQuery, OutputPort<QueryResult<ActiveSessionDto>>>
{
    private readonly IRefreshTokenQueryRepository _refreshTokenQueryRepository;

    public GetActiveSessionsQueryHandler(IRefreshTokenQueryRepository refreshTokenQueryRepository)
    {
        _refreshTokenQueryRepository = refreshTokenQueryRepository;
    }

    public async Task<OutputPort<QueryResult<ActiveSessionDto>>> Handle(GetActiveSessionsQuery query, CancellationToken cancellationToken)
    {
        var filter = new FilterActiveSessions
        {
            FromDate = query.Filter.FromDate,
            ToDate = query.Filter.ToDate,
            DeviceId = query.Filter.DeviceId
        };

        var results = await _refreshTokenQueryRepository.GetActiveSessionsAsync(
            query.UserId,
            query.Filter.NumberPage,
            query.Filter.PageSize,
            query.CurrentDeviceId,
            filter,
            cancellationToken);

        if (!results.Results.Any())
            return OutputPort<QueryResult<ActiveSessionDto>>.Success(data: null, statusCode: HttpStatusCode.NoContent);

        return OutputPort<QueryResult<ActiveSessionDto>>.Success(data: results);
    }
}