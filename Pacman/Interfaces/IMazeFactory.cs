namespace Pacman.Interfaces
{
    public interface IMazeFactory
    { 
        IMaze CreateMaze(string mazeData);
    }
}