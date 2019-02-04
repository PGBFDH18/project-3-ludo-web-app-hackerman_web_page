using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackerman_WebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace Hackerman_WebbApp.Controllers
{

    public class LudoController : Controller
    {
        private IGameModel game;

        public LudoController(IGameModel _game)
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
            game.SetGameId(int.Parse(output));

            return View(game);
        }
        
        [HttpPost("addplayer/{gameId}")]
        public async Task<IActionResult> AddPlayer(string gameId, [FromBody] List<Player> players)
        {
            //var playerList = JsonConvert.DeserializeObject<List<Player>>(players);
            var client = new RestClient("http://localhost:57659/");
            foreach (var item in players)
            {
                var response = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
                response.AddJsonBody(item);
                var restResponse = await client.ExecuteTaskAsync(response);
            }

            return Ok();
        }

        [HttpGet("listgames")]
        public async Task<IActionResult> ListGames()
        {
            var client = new RestClient("http://localhost:57659/");

            var response = new RestRequest("api/ludo", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            int[] output = JsonConvert.DeserializeObject<int[]>(restResponse.Content);
            GameList.SetGameList(output);

            return View(GameList.GetGameList());
        }

        [HttpGet("gameboard")]
        public IActionResult Gameboard()
        {
            return View();
        }
        
    }
}