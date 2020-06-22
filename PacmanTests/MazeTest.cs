using Pacman;
using Pacman.Enums;
using Xunit;

namespace PacmanTests
{
    public class MazeTest
    {
        [Theory]
        [InlineData(1, 11, 25)]
        [InlineData(2, 21, 19)]
        public void GivenLevelShouldBuildCorrectMazeSize(int level, int height, int width)
        {
            var fileReader = new FileReader();
            var maze = new Maze(level, fileReader);
            Assert.Equal(height, maze.Height);
            Assert.Equal(width, maze.Width);
        }

        [Fact]
        public void GivenLevelShouldPopulateBasedOnMazeData()
        {
            var fileReader = new FileReader();
            var maze = new Maze(1, fileReader);
            Assert.Equal(11, maze.Height);
            Assert.Equal(25, maze.Width);
            Assert.Equal(new Tile(TileType.Wall).Display, maze.MazeArray[0,2].Display);
            Assert.Equal(new Tile(TileType.Pellet).Display, maze.MazeArray[1,1].Display );
        }
    }
}