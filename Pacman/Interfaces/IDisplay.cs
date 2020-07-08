using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public interface IDisplay
    {
        void OutputMaze(IMaze maze);
        void GameStats(int score, int livesLeft);
        void LostLife(int livesLeft); 
        void Welcome();
        void CongratulationsNewLevel(int levelNumber);
        void GameEnd(int levelNumber);
    }
}