using AZTDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AZTDinner.Api.Controllers;
public class ErrorsController:ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
        //return Problem(title:exception.Message, statusCode:400);
        var (statusCode, message)=exception switch
        {
            IServiceException serviceException =>  ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _=>(StatusCodes.Status500InternalServerError, "An unexpected error occured."),
        };
        return Problem(statusCode: statusCode, title:message);
    }
}