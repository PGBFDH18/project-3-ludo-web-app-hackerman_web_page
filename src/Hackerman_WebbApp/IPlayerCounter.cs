namespace Hackerman_WebbApp
{
    public interface IPlayerCounter
    {
        int WhosTurn { get; set; }

        void UpdatePlayerTurn(int numberOfPlayers);
    }
}