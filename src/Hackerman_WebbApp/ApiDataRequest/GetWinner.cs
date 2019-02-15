using Hackerman_WebbApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.ApiDataRequest
{
    public static class GetWinner
    {
        public static async Task<Player> GetPlayer(IRestClient client, int gameId)
        {
            var response = new RestRequest($"api/ludo/{gameId}/winner", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            Player output = new Player();
            output = JsonConvert.DeserializeObject<Player>(restResponse.Content);
            return output;
        }
    }
}
