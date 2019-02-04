using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackerman_WebbApp.Models
{
    public class GameModel : IGameModel
    {
        private int GameId { get; set; }
        public List<Player> PlayerList { get; set; }

        public void SetGameId(int gameId) => this.GameId = gameId;

        public int GetGameId() => this.GameId;

        public void AddPlayer(Player player) => this.PlayerList.Add(player);
    }
}
