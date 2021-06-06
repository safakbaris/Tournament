using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Tourament.Web.Helpers;
using Tourament.Web.Models;
using Tournament.Core;
using Tournament.Core.Entities;

namespace Tourament.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService service;
        private readonly IMapper mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await service.Login(model.UserName, model.Password);
                if (user != null)
                {
                    HttpContext.Session.Set("user", user);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.Message = "Kullanıcı adı veya şifre hatalı.";
                }
            }

            return View();
        }




        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = mapper.Map<User>(model);
                var result = await service.Add(entity);
                if (result)
                {
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(entity));
                    return RedirectToAction("index", "home");
                }
            }
          
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

       
    }
}
