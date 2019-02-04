using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.Models
{
    public class GameList
    {
        private static int[] ListOfAllGames { get; set; }

        public static void SetGameList(int[] list)
        {
            ListOfAllGames = list;
        }
        public static int[] GetGameList()
        {
            return ListOfAllGames;
        }
    }
}
