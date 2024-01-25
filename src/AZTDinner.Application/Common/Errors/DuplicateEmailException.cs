using System.Net;

namespace AZTDinner.Application.Common.Errors;
public class DuplicateEmailExcetion : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email already exists.";
}