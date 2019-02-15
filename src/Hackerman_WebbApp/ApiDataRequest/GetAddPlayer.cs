using Hackerman_WebbApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class GetAddPlayer
    {
        public static async Task<bool> AddPlayer(IRestClient client, int gameId, Player player)
        {
            var response = new RestRequest($"api/ludo/{gameId}/players", Method.POST);
            response.AddJsonBody(player);
            var restResponse = await client.ExecuteTaskAsync(response);

            return true;
        }
    }
}
