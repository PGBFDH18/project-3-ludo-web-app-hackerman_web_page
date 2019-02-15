using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackerman_WebbApp.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Hackerman_WebbApp
{
    public static class GetPlayerInfo
    {



        public static async Task<List<Player>> GetPlayerPosition(int gameId, int numberOfPlayers, IRestClient client)
        {

            List<Player> playerList = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {

                var response = new RestRequest($"api/ludo/{gameId}/players/{i}", Method.GET);
                var restResponse = await client.ExecuteTaskAsync(response);
                playerList.Add(JsonConvert.DeserializeObject<Player>(restResponse.Content));
            }

            return playerList;
        }
    }
}
