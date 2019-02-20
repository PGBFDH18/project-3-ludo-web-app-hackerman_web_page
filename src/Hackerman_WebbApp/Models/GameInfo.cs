using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.Models
{
    public class GameInfo
    {
        public string State { get; set; }
        public int GameId { get; set; }
        public int NumberOfPlayers { get; set; }
        public int CurrentPlayerId { get; set; }

    }
}
