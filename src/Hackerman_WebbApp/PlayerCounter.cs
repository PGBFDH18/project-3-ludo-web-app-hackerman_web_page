using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp
{
    public class PlayerCounter : IPlayerCounter
    {
        public int WhosTurn { get; set; }



        public void UpdatePlayerTurn(int numberOfPlayers)
        {
            WhosTurn++;
            if (WhosTurn == numberOfPlayers)
            {
                WhosTurn = 0;
            }
        }
    }
}
