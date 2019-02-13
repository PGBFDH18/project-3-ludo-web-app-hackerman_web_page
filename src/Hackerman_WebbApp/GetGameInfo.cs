using Hackerman_WebbApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class GetGameInfo
    {

        public static async Task<GameModel> GetGame (IRestClient client, int gameId)
        {
            var response = new RestRequest($"api/ludo/{gameId}", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            GameModel output = JsonConvert.DeserializeObject<GameModel>(restResponse.Content);

            return output;
        }
    }
}
