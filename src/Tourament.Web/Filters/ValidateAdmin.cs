using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Tourament.Web.Helpers;
using Tournament.Core.Entities;
using static Tournament.Core.Enum.Enums;

namespace Tourament.Web.Filters
{
    public class ValidateAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.Get<User>("user");
            if (user.UserType != UserType.Admin)
                context.Result = new RedirectToActionResult("index", "home", "");

            base.OnActionExecuting(context);
        }
    }
}
