using AutoMapper;
using EnterpriseTemplateSolution.Entities.Identity;
using EnterpriseTemplateSolution.Services.Interfaces;
using EnterpriseTemplateSolution.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EnterpriseTemplateSolution.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegistrationUserDto registrationUserDto)
    {
        var user = _mapper.Map<ApplicationUser>(registrationUserDto);
        var result = await _userManager.CreateAsync(user, registrationUserDto.Password);

        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, registrationUserDto.Roles);

        return result;
    }
}