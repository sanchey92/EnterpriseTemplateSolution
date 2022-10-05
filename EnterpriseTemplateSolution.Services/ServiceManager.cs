using AutoMapper;
using EnterpriseTemplateSolution.Entities.Identity;
using EnterpriseTemplateSolution.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EnterpriseTemplateSolution.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _authenticationService =
            new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, userManager, configuration));
    }

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}