using Hackerman_WebbApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class GetMovePiece
    {
        public static async void MovePiece(IRestClient client, MovePiece piece, int gameId)
        {
            var response = new RestRequest($"api/ludo/{gameId}", Method.PUT);
            response.AddJsonBody(piece);
            var restResponse = await client.ExecuteTaskAsync(response);
        }
    }
}
