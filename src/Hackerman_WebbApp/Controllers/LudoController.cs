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
        private IPlayerCounter counter;

        public LudoController(IRestClient _client, IPlayerCounter _counter)
        {
            client = _client;
            client.BaseUrl = new Uri("https://ludoapi.azurewebsites.net/");
            counter = _counter;
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
            return View(new Player());
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


            return View("Newgame", new Player { Id = +1 });
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
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameModel output = JsonConvert.DeserializeObject<GameModel>(restResponse.Content);
            
            return output;
        }

        [HttpGet("gameboard")]
        public async Task<IActionResult> Gameboard()
        {
            var gameId = HttpContext.Session.GetInt32("game");

            var response = new RestRequest($"api/ludo/{gameId}/state", Method.PUT);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameModel output = new GameModel();
            var response2 = new RestRequest($"api/ludo/{gameId}/players/0", Method.GET);
            var restResponse2 = await client.ExecuteTaskAsync(response2);
            output.Player = JsonConvert.DeserializeObject<Player>(restResponse2.Content);
            return View(output);
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


        [HttpGet("allplayerinfo")]
        public async Task<IActionResult> AllPlayerInfo(int playerId)
        {
            GameModel currentGameInfo = await GameInfo();
            var gameId = HttpContext.Session.GetInt32("game");

            
            currentGameInfo.PlayerList = await GetPlayerInfo.GetPlayerPosition(currentGameInfo.NumberOfPlayers, (int)gameId, client);

            return View("Gameboard", currentGameInfo);
        }

        [HttpGet("movepiece")]
        public async Task<IActionResult> MovePiece(GameModel htmlModel)
        {
            var gameId = HttpContext.Session.GetInt32("game");

            var response3 = new RestRequest($"api/ludo/{gameId}", Method.GET);
            var restResponse3 = await client.ExecuteTaskAsync(response3);
            GameModel output = JsonConvert.DeserializeObject<GameModel>(restResponse3.Content);
            output.MovePiece = new MovePiece();
            output.WhosTurn = new PlayerCounter();
            output.MovePiece.PlayerId = counter.WhosTurn;
     

            var response = new RestRequest($"api/ludo/{gameId}/players/{output.MovePiece.PlayerId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            output.Player = JsonConvert.DeserializeObject<Player>(restResponse.Content);

           // output.MovePiece.PlayerId = output.Player.Id;
            output.MovePiece.PieceId = htmlModel.MovePiece.PieceId;
            output.MovePiece.NumberOfFields = 5;
            var response2 = new RestRequest($"api/ludo/{gameId}", Method.PUT);
            response2.AddJsonBody(output.MovePiece);
            var restResponse2 = await client.ExecuteTaskAsync(response2);

            output.PlayerList = await GetPlayerInfo.GetPlayerPosition((int)gameId, output.NumberOfPlayers, client);

            counter.UpdatePlayerTurn(output.NumberOfPlayers);

            output.Player = output.PlayerList[counter.WhosTurn];

            return View("Gameboard", output);
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