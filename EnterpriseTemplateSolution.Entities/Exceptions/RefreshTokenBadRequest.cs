namespace EnterpriseTemplateSolution.Entities.Exceptions;

public class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base("Invalid request. The TokenDto has some invalid values")
    {
        
    }
}