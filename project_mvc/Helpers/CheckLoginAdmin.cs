using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace project_mvc.Helpers
{
    public class CheckLoginAdmin : ActionFilterAttribute
    {
        private HttpRequest? Request;
        private HttpResponse? Response;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            Request = context.Request;
            Response = context.Response;
            string? path = Request.Path.Value;
            bool user = true;

            if (!user)
            {
                if (path?.Trim('/') != WebConfig.AdminAlias)
                    filterContext.Result = new RedirectResult("/" + WebConfig.AdminAlias + "?returnUrl=" + Request.Host + Request.Path);
            }
            else
                if (path?.Trim('/') == WebConfig.AdminAlias)
                filterContext.Result = new RedirectResult("/" + WebConfig.AdminAlias + "/HomeAdmin/Index");
        }
    }
}
