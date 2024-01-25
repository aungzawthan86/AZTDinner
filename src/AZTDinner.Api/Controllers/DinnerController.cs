
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AZTDinner.Api.Controllers;

[Route("[controller]")]
public class DinnerController : ApiController
{
    [HttpGet]
    public IActionResult ListDinner()
    {
        return Ok(Array.Empty<string>());
    }
}