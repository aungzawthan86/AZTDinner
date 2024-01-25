using System.Net;

namespace AZTDinner.Application.Common.Errors;

public interface IServiceException
{
    public HttpStatusCode StatusCode{get;}
    public string ErrorMessage{get;}
}