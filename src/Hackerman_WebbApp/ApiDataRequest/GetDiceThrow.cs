using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class GetDiceThrow
    {

        public static async Task<int> RollDice(IRestClient client)
        {

            var response = new RestRequest("api/ludo/rolldice", Method.GET);
            var restResponse = await client.ExecuteTaskAsync(response);
            int output = int.Parse(restResponse.Content);
            return output;
        }


    }
}
