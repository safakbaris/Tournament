using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tourament.Web.Filters;
using Tourament.Web.Helpers;
using Tournament.Core;
using Tournament.Core.Entities;

namespace Tourament.Web.Controllers
{
    [ValidateSession]
    public class GameController : Controller
    {
        private readonly IUserService userService;
        private readonly IGameService gameService;

        public GameController(IUserService userService, IGameService gameService)
        {
            this.userService = userService;
            this.gameService = gameService;
        }
        public async Task<IActionResult> Index([FromRoute] int id)
        {

            if (id > 0)
            {
                var user = HttpContext.Session.Get<User>("user");


                var authenticationResult = await userService.IsUserAuthenticatedForGame(user.Id, id);
                if (!authenticationResult.Success)
                    ViewBag.ErrorMessage = authenticationResult.Message;
                else
                {
                    var gameInfo = await gameService.GetGameInfo(id);
                    if (gameInfo != null)
                    {
                        ViewBag.Number = gameInfo.Number;
                        ViewBag.GameId = id;
                    }
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Play([FromQuery] int gameId, [FromQuery] int number = 0)
        {

            var user = HttpContext.Session.Get<User>("user");
            var authenticationResult = await userService.IsUserAuthenticatedForGame(user.Id, gameId);
            if (authenticationResult.Success)
                return Json(await gameService.Play(gameId, user.Id, number));
            else
                return Json(authenticationResult);


            return Json(new { Success = false });
        }




        //[HttpPost]
        //public async Task<ActionResult> Substract([FromBody] SubstractModel model)
        //{
        //    bool result = false; string message = "";
        //    var user = HttpContext.Session.Get<User>("user");


        //    return Json(new { Success = result, Message = message });

        //}
    }
}
