using Hackerman_WebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public static class PlayerColor
    {

        public static void SetPlayerColor(Player player)
        {
            switch (player.Id)
            {
                case 0:
                    player.Color = "Red";
                    break;
                case 1:
                    player.Color = "Green";
                    break;
                case 3:
                    player.Color = "Yellow";
                    break;
                case 2:
                    player.Color = "Blue";
                    break;
            }

        }

    }

}
