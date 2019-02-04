using System.Collections.Generic;

namespace Hackerman_WebbApp.Models
{
    public interface IGameModel
    {
        void SetGameId(int gameId);
        int GetGameId();
    }
}