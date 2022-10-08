using EnterpriseTemplateSolution.Services.Interfaces;
using EnterpriseTemplateSolution.Shared.DTOs.AuthenticationService;
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

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationUserDto authenticationUserDto)
    {
        if (!await _service.AuthenticationService.ValidateUserAsync(authenticationUserDto))
            return Unauthorized();

        var tokenDto = await _service.AuthenticationService.CreateTokenAsync(populateExp: true);

        return Ok(tokenDto);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenDto tokenDto)
    {
        var tokenToReturnDto = await _service.AuthenticationService.RefreshTokenAsync(tokenDto);
        return Ok(tokenToReturnDto);
    }
}