using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Hackerman_WebbApp.Controllers
{

    public class LudoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> NewGame()
        {
            var client = new RestClient("http://localhost:57659/");

            var response = new RestRequest("api/ludo", Method.POST);
            var restResponse = await client.ExecuteTaskAsync(response);
            var output = restResponse.Content;

            return View(Gameboard(output));
        }
        
        [HttpGet("gameboard")]
        public IActionResult Gameboard(string gameId)
        {
            return View();
        }
        
    }
}