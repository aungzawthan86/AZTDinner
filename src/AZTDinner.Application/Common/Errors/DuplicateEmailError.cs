using System.Net;
using FluentResults;

namespace AZTDinner.Application.Common.Errors;
public class DuplicateEmailError : IError
{
    public List<IError> Reasons => throw new NotImplementedException();

    public string Message => throw new NotImplementedException();

    public Dictionary<string, object> Metadata => throw new NotImplementedException();
}
