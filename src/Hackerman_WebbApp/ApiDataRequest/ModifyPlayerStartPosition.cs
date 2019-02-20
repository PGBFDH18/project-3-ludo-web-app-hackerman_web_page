using Hackerman_WebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class ModifyPlayerStartPosition
    {

        public static void SetStartPosition(GameModel game)
        {
            foreach (var item in game.PlayerList)
            {
                switch (item.Color)
                {
                    case "Red":
                        item.StartPosition = 0;
                        break;
                    case "Green":
                        item.StartPosition = 13;
                        break;
                    case "Blue":
                        item.StartPosition = 26;
                        break;
                    case "Yellow":
                        item.StartPosition = 39;
                        break;
                }
            }
        }


    }
}
