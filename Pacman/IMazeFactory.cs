namespace Pacman
{
    public interface IMazeFactory
    { 
        IMaze CreateMaze(string[] mazeData);
    }

    public class MazeFactory : IMazeFactory
    {
        public IMaze CreateMaze(string[] mazeData)
        {
            return new Maze(mazeData);
        }
    }
}