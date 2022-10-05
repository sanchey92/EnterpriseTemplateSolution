using AutoMapper;
using EnterpriseTemplateSolution.Entities.Identity;
using EnterpriseTemplateSolution.Shared;
using EnterpriseTemplateSolution.Shared.DTOs.AuthenticationService;

namespace EnterpriseTemplateSolution.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationUserDto, ApplicationUser>();
    }
}