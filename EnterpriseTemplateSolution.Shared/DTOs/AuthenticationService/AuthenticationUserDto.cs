namespace EnterpriseTemplateSolution.Shared.DTOs.AuthenticationService;

public record AuthenticationUserDto
{
    public string? Email { get; init; }
    public string? Password { get; set; }
}