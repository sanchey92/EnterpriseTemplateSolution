using AutoMapper;
using EnterpriseTemplateSolution.Entities.Identity;
using EnterpriseTemplateSolution.Shared;

namespace EnterpriseTemplateSolution.API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationUserDto, ApplicationUser>();
    }
}