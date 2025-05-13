using CentralAPIs.IRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CentralAPIs.CustomeActionFilter
{
    public class Validate : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ValidateToken>();
        }
    }

    public class ValidateToken: ActionFilterAttribute
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidateToken(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var headers = request.Headers;
            string token;
            try
            {
                token = headers["AuthenticationKey"];

                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedObjectResult("Authentication token is missing.");
                    return;
                }
                var _sessionRepo = _serviceProvider.GetRequiredService<ISessionRepo>();

                var (valid,msg)= _sessionRepo.validateSession(token);

                if (!valid)
                {
                    context.Result = new UnauthorizedObjectResult(msg);
                    return;
                }
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult("Token Not Fonund");
                return;
            }


            base.OnActionExecuting(context);
        }
    }
}
