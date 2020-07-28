namespace Pacman.Interfaces
{
    public interface IMazeFactory
    { 
        IMaze CreateMaze(GameSettings gameSettings, int currentLevelNumber);
    }
}