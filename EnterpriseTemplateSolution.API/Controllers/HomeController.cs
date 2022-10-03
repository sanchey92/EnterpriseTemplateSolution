using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplateSolution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public string SayHello() => "Hello world!!!";
}