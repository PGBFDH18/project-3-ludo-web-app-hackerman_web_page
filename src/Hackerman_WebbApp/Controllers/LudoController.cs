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
using Hackerman_WebbApp.ApiDataRequest;
using Microsoft.AspNetCore.Localization;

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
            Log.Information($"Client connected on remote IP: {HttpContext.Connection.RemoteIpAddress.ToString()}");
            return View();
        }

        [HttpPost("newgame")]
        public async Task<IActionResult> NewGame()
        {
            byte[] gameId;
            GameModel game = new GameModel();
            if (!HttpContext.Session.TryGetValue("game", out gameId))
            {
                var response = new RestRequest("api/ludo", Method.POST);
                var restResponse = await client.ExecuteTaskAsync(response);
                var output = restResponse.Content;
                game.GameId = int.Parse(output);
                HttpContext.Session.SetInt32("game", game.GameId);
                Log.Information($"GameID: {game.GameId} was created.(IP: {HttpContext.Connection.RemoteIpAddress.ToString()})");

            }
            game.Player = new Player() { Id = 0 };

            return View(game);
        }

        [HttpPost("addplayer")]
        public async Task<IActionResult> AddPlayer(Player player)
        {
            PlayerColor.SetPlayerColor(player);
            HttpContext.Session.SetString("name", player.Name);
            var gameId = HttpContext.Session.GetInt32("game");

            await GetAddPlayer.AddPlayer(client, (int)gameId, player);
            GameModel output = await GetGameInfo.GetGame(client, (int)gameId);
            Log.Information($"GameID: {gameId} created a player named: {player.Name} with the color: {player.Color}(IP: {HttpContext.Connection.RemoteIpAddress.ToString()})");

            output.PlayerList = await GetPlayerInfo.GetPlayerPosition((int)gameId, output.NumberOfPlayers, client);
            output.Player = new Player() { Id = player.Id + 1 };
            return View("Newgame", output);

        }

        [HttpGet("listgames")]
        public async Task<IActionResult> ListGames()
        {

            var response = new RestRequest("api/ludo", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameList output = new GameList() { ListOfAllGames = JsonConvert.DeserializeObject<int[]>(restResponse.Content) };

            return View(output);
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

        [HttpGet("rolldice")]
        public async Task<IActionResult> RollDice()
        {
            var gameId = HttpContext.Session.GetInt32("game");
            GameModel output = await GetGameInfo.GetGame(client, (int)gameId);

            output.Player = await GetCurrentPlayer.GetPlayer(client, (int)gameId, counter);
            output.DiceThrow = await GetDiceThrow.RollDice(client);

            output.PlayerList = await GetPlayerInfo.GetPlayerPosition((int)gameId, output.NumberOfPlayers, client);
            ModifyPlayerStartPosition.SetStartPosition(output);

            return View("gameboard", output);
        }

        [HttpGet("movepiece")]
        public async Task<IActionResult> MovePiece(GameModel htmlModel)
        {
            var gameId = HttpContext.Session.GetInt32("game");

            GameModel output = await GetGameInfo.GetGame(client, (int)gameId);
            output.MovePiece = new MovePiece();
            output.WhosTurn = counter;
            output.MovePiece.PlayerId = counter.WhosTurn;

            output.Player = await GetCurrentPlayer.GetPlayer(client, (int)gameId, counter);

            output.MovePiece.PieceId = htmlModel.MovePiece.PieceId - 1;
            output.MovePiece.NumberOfFields = htmlModel.DiceThrow;
            await GetMovePiece.MovePiece(client, output.MovePiece, (int)gameId);

            output.PlayerList = await GetPlayerInfo.GetPlayerPosition((int)gameId, output.NumberOfPlayers, client);

            counter.UpdatePlayerTurn(output.NumberOfPlayers);

            output.Player = output.PlayerList[counter.WhosTurn];
            output.Winner = await GetWinner.GetPlayer(client, (int)gameId);
            if (output.Winner != null)
            {
                Log.Information($"{output.Winner.Name} won the game: {gameId}! (IP: {HttpContext.Connection.RemoteIpAddress.ToString()})");
            }
            ModifyPlayerStartPosition.SetStartPosition(output);

            return View("Gameboard", output);
        }

        [HttpPost("joingame")]
        public async Task<IActionResult> JoinGame(int gameId)
        {

            HttpContext.Session.SetInt32("game", gameId);

            var response = new RestRequest($"api/ludo/{gameId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameModel output = JsonConvert.DeserializeObject<GameModel>(restResponse.Content);

            output.WhosTurn = counter;

            output.Player = await GetCurrentPlayer.GetPlayer(client, gameId, counter);

            output.PlayerList = await GetPlayerInfo.GetPlayerPosition(gameId, output.NumberOfPlayers, client);

            return View("gameboard", output);
        }

        [HttpPost]
        public  IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(2) });

            //  return LocalRedirect(returnUrl);
            return View();
        }
    }
}