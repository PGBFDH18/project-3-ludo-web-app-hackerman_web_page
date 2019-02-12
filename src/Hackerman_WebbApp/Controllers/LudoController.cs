using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackerman_WebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Serilog;

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
                Log.Information("Game was created with ID: {gameId}", game.GameId);
            }
            return View();
        }

        [HttpPost("addplayer")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("name", player.Name);
                var gameId = HttpContext.Session.GetInt32("game");

           
                var response = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
                response.AddJsonBody(player);
                var restResponse = await client.ExecuteTaskAsync(response);
            }


            return View("Newgame", player);
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
        public async Task<IActionResult> Gameboard()
        {
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}/state", Method.PUT);
            var restResponse = await client.ExecuteTaskAsync(response);

            return View();
        }

        [HttpGet("playerinfo/{playerId}")]
        public async Task<IActionResult> PlayerInfo(int playerId)
        {
            GameModel output = new GameModel();
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}/players/{playerId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            output.Player = JsonConvert.DeserializeObject<Player>(restResponse.Content);

            return View("Gameboard");
        }

        [HttpGet("movepiece/{playerId}/{pieceId}")]
        public async Task<IActionResult> MovePiece(int playerId, int pieceId)
        {
            GameModel output = new GameModel() { MovePiece = new MovePiece() };
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}/players/{playerId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            output.Player = JsonConvert.DeserializeObject<Player>(restResponse.Content);

            output.MovePiece.PlayerId = output.Player.Id;
            output.MovePiece.PieceId = pieceId;
            output.MovePiece.NumberOfFields = 5;
            var response2 = new RestRequest($"api/ludo/{gameId}", Method.PUT);
            response2.AddJsonBody(output.MovePiece);
            var restResponse2 = await client.ExecuteTaskAsync(response2);

            return View("Gameboard");
        }

        [HttpGet("getplayers/{gameId}/players")]
        public async Task<IActionResult> GetPlayers()
        {
            GameModel output = new GameModel();
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}/players", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            output.Player = JsonConvert.DeserializeObject<Player>(restResponse.Content);

            return Ok();
        }
    }
}