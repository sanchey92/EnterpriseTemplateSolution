using EnterpriseTemplateSolution.Shared.DTOs.AuthenticationService;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseTemplateSolution.Services.Interfaces;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUserAsync(RegistrationUserDto registrationUserDto);
    Task<bool> ValidateUserAsync(AuthenticationUserDto authenticationUserDto);
    Task<string> CreateTokenAsync();
}