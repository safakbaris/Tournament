using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Tourament.Web.Filters
{
    public class ValidateSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJsonString = context.HttpContext.Session.GetString("user");
            if (String.IsNullOrEmpty(userJsonString))
                context.Result = new RedirectToActionResult("login", "user", "");

            base.OnActionExecuting(context);
        }
    }
}
