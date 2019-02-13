using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.Models
{
    public class GameModel
    {
        public int GameId { get; set; }
        public string State { get; set; }
        public int NumberOfPlayers { get; set; }
        public int CurrentPlayerId { get; set; }
        public Player Player { get; set; }
        public MovePiece MovePiece { get; set; }

        public List<Player> PlayerList { get; set; }

        public PlayerCounter WhosTurn { get; set; }

        public int DiceThrow { get; set; }
    }
}
