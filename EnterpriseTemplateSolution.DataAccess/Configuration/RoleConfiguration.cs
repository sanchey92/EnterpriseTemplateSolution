using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseTemplateSolution.DataAccess.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Name = "DefaultUser",
                NormalizedName = "DEFAULT_USER"
            },
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}