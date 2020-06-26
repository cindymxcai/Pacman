using System;
using System.IO;
using Newtonsoft.Json;
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
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<LevelObject>(json);
            var maze = new Maze(new FileReader(), levels, level);
            Assert.Equal(height, maze.Height);
            Assert.Equal(width, maze.Width);
        }

        [Fact]
        public void GivenLevelShouldPopulateBasedOnMazeData()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<LevelObject>(json);
            var maze = new Maze(new FileReader(), levels, 1);
            Assert.Equal(11, maze.Height);
            Assert.Equal(25, maze.Width);
            Assert.Equal(new Tile(TileType.Wall).Display, maze.MazeArray[0,2].Display);
            Assert.Equal(new Tile(TileType.Pellet).Display, maze.MazeArray[1,1].Display );
        }
    }
}