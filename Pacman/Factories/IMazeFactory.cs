namespace Pacman.Factories
{
    public interface IMazeFactory
    { 
        IMaze CreateMaze(GameSettings gameSettings, int currentLevelNumber);
    }

    public class MazeFactory : IMazeFactory
    {
        private readonly IFileReader _fileReader;

        public MazeFactory(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        public IMaze CreateMaze( GameSettings gameSettings, int currentLevelNumber )
        {
            var mazeData = _fileReader.ReadFile(gameSettings.LevelSettings[currentLevelNumber - 1]);
            return new Maze(mazeData);
        }
    }
}