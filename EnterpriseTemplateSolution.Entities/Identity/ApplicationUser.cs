using Microsoft.AspNetCore.Identity;

namespace EnterpriseTemplateSolution.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}