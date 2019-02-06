using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackerman_WebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Hackerman_WebbApp.Controllers
{

    public class LudoController : Controller
    {
        private IRestClient client;
    

        public LudoController(IRestClient _client)
        {
            client = _client;
            client.BaseUrl = new Uri("https://ludoapi.azurewebsites.net/");

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> NewGame()
        {
            byte[] gameId;
            if (!HttpContext.Session.TryGetValue("game", out gameId))
            {
                var response = new RestRequest("api/ludo", Method.POST);
                var restResponse = await client.ExecuteTaskAsync(response);
                var output = restResponse.Content;
                GameModel game = new GameModel() { GameId = int.Parse(output) };
                HttpContext.Session.SetInt32("game", game.GameId);

            }
            return View();
        }

        [HttpPost("addplayer")]
        public async Task<IActionResult> AddPlayer(Player player)
        {
            var gameId = HttpContext.Session.GetInt32("game");

           
            var response = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
            response.AddJsonBody(player);
            var restResponse = await client.ExecuteTaskAsync(response);

            return View("Newgame");
        }

        [HttpGet("listgames")]
        public async Task<IActionResult> ListGames()
        {

            var response = new RestRequest("api/ludo", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameList output = new GameList() { ListOfAllGames = JsonConvert.DeserializeObject<int[]>(restResponse.Content) };

            return View(output);
        }

        [HttpGet("gameinfo")]
        public async Task<GameModel> GameInfo()
        {
            //if (GetInfo == "info")
            //{
                var gameId = HttpContext.Session.GetInt32("game");

                var response = new RestRequest($"api/ludo/{gameId}", Method.GET);
                var restResponse = await client.ExecuteTaskAsync(response);
                GameModel output = JsonConvert.DeserializeObject<GameModel>(restResponse.Content);

            //    return Ok();
            //}
            return output;
        }

        [HttpGet("gameboard")]
        public IActionResult Gameboard()
        {
            return View();
        }

    }
}