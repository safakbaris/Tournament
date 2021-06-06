using Microsoft.AspNetCore.Mvc;
using Tourament.Web.Filters;
using Tourament.Web.Helpers;
using Tournament.Core.Entities;
using static Tournament.Core.Enum.Enums;

namespace Tourament.Web.Controllers
{
    [ValidateSession]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }


        public IActionResult Index()
        {
            var user = HttpContext.Session.Get<User>("user");
            if (user.UserType == UserType.Admin)
                ViewBag.IsAdmin = true;
            else
                ViewBag.IsAdmin = false;
            return View();
        }


    }
}
