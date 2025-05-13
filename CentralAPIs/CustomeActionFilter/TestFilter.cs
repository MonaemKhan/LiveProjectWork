using Microsoft.AspNetCore.Mvc.Filters;

namespace CentralAPIs.CustomeActionFilter
{
    public class TestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var headers = request.Headers;

            var responce = context.HttpContext.Response;
            Console.WriteLine($"[{DateTime.Now}] Incoming request: {request.Method} {request.Path}");

            base.OnActionExecuting(context);
        }
    }
}
