using EnterpriseTemplateSolution.API.Definitions.Base;
using EnterpriseTemplateSolution.Entities.ErrorModel;
using EnterpriseTemplateSolution.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace EnterpriseTemplateSolution.API.Definitions.Errors;

public class ExceptionsDefinition : ApplicationDefinition
{
    public override void ConfigureApplication(WebApplication application, IWebHostEnvironment environment)
    {
        application.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "applicationJson";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}