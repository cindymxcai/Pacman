using Pacman.Interfaces;

namespace Pacman.Factories
{
    public interface IMazeFactory
    { 
        IMaze CreateMaze(GameSettings gameSettings, int currentLevelNumber);
    }
}