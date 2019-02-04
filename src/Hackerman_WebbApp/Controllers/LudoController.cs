using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackerman_WebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Hackerman_WebbApp.Controllers
{

    public class LudoController : Controller
    {
        private readonly GameModel game;

        public LudoController(GameModel _game)
        {
            game = _game;
        }

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
            game.GameId = int.Parse(output);

            return View(game);
        }
        
        [HttpPost("addplayer")]
        public async Task<IActionResult> AddPlayer(string gameId, List<Player> players)
        {
            var client = new RestClient("http://localhost:57659/");
            var output = "";
            foreach (var item in players)
            {
                var response = new RestRequest("api/ludo/{gameId}/players", Method.POST);
                response.AddJsonBody(item);
                var restResponse = await client.ExecuteTaskAsync(response);
                output += restResponse.Content;
            }

            return Ok();
        }

        [HttpGet("gameboard")]
        public IActionResult Gameboard()
        {
            return View();
        }
        
    }
}