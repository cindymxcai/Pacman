namespace Pacman.Interfaces
{
    public interface IDisplay
    {
        void GameStats(int score, int livesLeft);
        void LostLife(int livesLeft); 
        void Welcome();
        void CongratulationsNewLevel(int levelNumber);
        void GameEnd(int levelNumber);
    }
}