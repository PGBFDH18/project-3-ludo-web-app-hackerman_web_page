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
        private IRestClient client;

        public LudoController(IRestClient _client)
        {
            client = _client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> NewGame()
        {
            client = new RestClient("https://ludoapi.azurewebsites.net/");

            var response = new RestRequest("api/ludo", Method.POST);
            var restResponse = await client.ExecuteTaskAsync(response);
            var output = restResponse.Content;
            var game = new GameModel(int.Parse(output));

            return View(game);
        }
        
        [HttpPost("addplayer/{gameId}")]
        public async Task<IActionResult> AddPlayer(GameModel game, List<Player> players)
        {
            var playerList = players;
            //var playerList = JsonConvert.DeserializeObject<List<Player>>(players);
            var client = new RestClient("https://ludoapi.azurewebsites.net/");
            foreach (var item in players)
            {
                var response = new RestRequest($"api/ludo/{game.GameId}/players", Method.POST);
                response.AddJsonBody(item);
                var restResponse = await client.ExecuteTaskAsync(response);
            }

            return Ok();
        }

        [HttpGet("listgames")]
        public async Task<IActionResult> ListGames()
        {
            var client = new RestClient("https://ludoapi.azurewebsites.net/");

            var response = new RestRequest("api/ludo", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameList output = new GameList() { ListOfAllGames = JsonConvert.DeserializeObject<int[]>(restResponse.Content) };
            
            return View(output);
        }

        [HttpGet("gameboard")]
        public IActionResult Gameboard()
        {
            return View();
        }
        
    }
}