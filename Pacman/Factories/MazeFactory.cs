using Pacman.Interfaces;

namespace Pacman.Factories
{
    public class MazeFactory : IMazeFactory
    {
        private readonly IFileReader _fileReader;
        private readonly ITileTypeFactory _tileTypeFactory;

        public MazeFactory(IFileReader fileReader, ITileTypeFactory tileTypeFactory)
        {
            _fileReader = fileReader;
            _tileTypeFactory = tileTypeFactory;
        }
        public IMaze CreateMaze( GameSettings gameSettings, int currentLevelNumber )
        {
            var mazeData = _fileReader.ReadFile(gameSettings.LevelSettings[currentLevelNumber - 1]);
            return new Maze(mazeData, _tileTypeFactory );
        }
    }
}