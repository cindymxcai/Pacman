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
        public IMaze CreateMaze(string mazeData)
        {
            var mazeDataForLevel = _fileReader.ReadFile(mazeData);
            return new Maze(mazeDataForLevel, _tileTypeFactory );
        }
    }
}