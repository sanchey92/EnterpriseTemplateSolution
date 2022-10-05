using EnterpriseTemplateSolution.Shared;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseTemplateSolution.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUserAsync(RegistrationUserDto registrationUserDto);
}