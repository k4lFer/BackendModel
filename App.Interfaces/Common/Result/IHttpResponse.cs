using System.Net;

namespace App.Interfaces.Common.Result;

public interface IHttpResponse
{
    HttpStatusCode StatusCode { get; }
}