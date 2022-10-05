using EnterpriseTemplateSolution.Services.Interfaces;
using EnterpriseTemplateSolution.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseTemplateSolution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthenticationController(IServiceManager service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrationUserDto registerDto)
    {
        var result = await _service.AuthenticationService.RegisterUserAsync(registerDto);

        if (!result.Succeeded)
            return BadRequest(ModelState);

        return StatusCode(201);
    }
}