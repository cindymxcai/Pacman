using System;
using System.IO;
using Newtonsoft.Json;
using Pacman;
using Pacman.Enums;
using Pacman.Sprites;
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
            var levels = JsonConvert.DeserializeObject<LevelData>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.Levels[level-1]);
            var maze = new Maze(mazeData);
            Assert.Equal(height, maze.Height);
            Assert.Equal(width, maze.Width);
        }

        [Fact]
        public void GivenLevelShouldPopulateBasedOnMazeData()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<LevelData>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.Levels[0]);
            var maze = new Maze(mazeData);
            Assert.Equal(11, maze.Height);
            Assert.Equal(25, maze.Width);
            Assert.Equal(new Tile(TileType.Wall).Display, maze.MazeArray[0,2].Display);
            Assert.Equal(new Tile(TileType.Pellet).Display, maze.MazeArray[1,1].Display );
        }

        [Fact]
        public void LevelSettingsFileShouldReturnCorrectInfo()
        {
            var levelData = Game.GetLevelData();
            Assert.Equal(3, levelData.MaxLevels);
        }
    }
}