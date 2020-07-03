using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public interface IDisplay
    {
        void MazeOutput(IMaze maze);
        void GameStats(int score, int livesLeft);
        void LostLife(int livesLeft);
        void UpdatePacmanDisplay(bool isChomping, IMaze gameMaze, ISprite pacman, Direction direction);
        void Welcome();
        void CongratulationsNewLevel(int levelNumber);
        void GameEnd(int levelNumber);
    }
}