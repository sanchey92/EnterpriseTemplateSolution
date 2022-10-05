using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EnterpriseTemplateSolution.Entities.Identity;
using EnterpriseTemplateSolution.Services.Interfaces;
using EnterpriseTemplateSolution.Shared.DTOs.AuthenticationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EnterpriseTemplateSolution.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private ApplicationUser _applicationUser;

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

    public async Task<bool> ValidateUserAsync(AuthenticationUserDto authenticationUserDto)
    {
        _applicationUser = await _userManager.FindByEmailAsync(authenticationUserDto.Email);
        var result = _applicationUser is not null
                     && await _userManager.CheckPasswordAsync(_applicationUser, authenticationUserDto.Password);

        return result;
    }

    public async Task<string> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<IEnumerable<Claim>> GetClaims()
    {
        var userClaims = await _userManager.GetClaimsAsync(_applicationUser);
        var roles = await _userManager.GetRolesAsync(_applicationUser);
        var rolesClaims = roles.Select(role => new Claim("roles", role)).ToList();

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, _applicationUser.UserName),
                new(JwtRegisteredClaimNames.Email, _applicationUser.Email),
                new("uid", _applicationUser.Id)
            }
            .Union(userClaims)
            .Union(rolesClaims);

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSection");

        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }
}