namespace Pacman
{
    public interface ILevel
    {
        int LivesLeft { get; set; }
        bool HasWon { get; }
        int LevelScore { get; }
        IGameEngine GameEngine { get; }
        IGameLogicValidator GameLogicValidator { get; }
        void PlayLevel();
        void HandleDeath();
    }
}