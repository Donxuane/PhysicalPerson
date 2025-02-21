using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhysicalPersonsApp.CustomActionFilter;

public class CheckRequestFilter: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if(string.IsNullOrEmpty(context.HttpContext.Request.Path.Value) || !context.HttpContext.Request.Path.HasValue)
        {
            context.Result = new BadRequestObjectResult(new
            {
                error = "Invalid Request Path!!"
            });
            return;
        }
        if(context.RouteData == null || context.RouteData.Values.Count == 0)
        {
            context.Result = new BadRequestObjectResult(new
            {
                error = "Invalid Route Data!"
            });
        }
        base.OnActionExecuting(context);
    }
}
