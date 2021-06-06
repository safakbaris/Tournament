using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tourament.Web.Filters;
using Tourament.Web.Helpers;
using Tourament.Web.Models;
using Tournament.Core;
using Tournament.Core.Dtos;
using Tournament.Core.Entities;
using static Tournament.Core.Enum.Enums;

namespace Tourament.Web.Controllers
{
    [ValidateSession]
    public class TournamentController : Controller
    {
        private readonly ITournamentService service;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TournamentController(ITournamentService service, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        [ValidateAdmin]
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAdmin]
        [HttpPost]
        public async Task<ActionResult> Create(CreateTournamentModel model)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Session.Get<User>("user");

                if (user.UserType == UserType.Admin)
                {
                    var httpContext = httpContextAccessor.HttpContext;
                    var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";

                    var result = await service.Add(mapper.Map<Tournament.Core.Entities.Tournament>(model), baseUrl);
                    if (result)
                        return RedirectToAction("Index", "Home");
                    else
                        ViewBag.Message = "Tournament cannot be created";
                }
                else
                {
                    ViewBag.Message = "Only admin can be created";
                }
            }
           
           
           

            return View();

        }

        [HttpPost]
        public async Task<ActionResult> Join([FromBody] JoinTournamentModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = mapper.Map<JoinTournamentDto>(model);
                var user = HttpContext.Session.Get<User>("user");
                dto.PlayerId = user.Id;
                return Json(await service.JoinTournament(dto));
            }

            return Json(new { Success= false});
        }
        [ValidateAdmin]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id>0)
                return Json(new { Success = await service.Remove(id) });

                return Json(new { Success = false });
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = HttpContext.Session.Get<User>("user");
            return Json(new { Tournaments = await service.Get(), UserType = user.UserType });
        }
    }
}
