using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldAuthorization.Controllers;


[ApiController]
[Route("/api/HelloWorld")]
[Authorize]
public class HelloWorldController : ControllerBase
{
    [HttpGet("Get")]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}