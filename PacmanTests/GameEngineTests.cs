using System;
using System.IO;
using Moq;
using Newtonsoft.Json;
using Pacman;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;
using Xunit;

namespace PacmanTests
{
    public class GameEngineTests
    {
        private static ITileTypeFactory SetUp()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile);
        }
        
        private static Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            return new Maze(mazeData, SetUp());
        }

        [Fact]
        public void UpdatesMazeWithCorrectTile()
        {
            var maze = MazeSetUp();

            var tileTypeFactory = SetUp();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( tileTypeFactory,  level.Pacman, maze);
            level.GameEngine.UpdateMazeTileDisplays(tileTypeFactory, maze, level.Pacman, level.Ghosts);

            Assert.Equal(new EmptyTile().Display, maze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].TileType.Display);
            var tile = new Tile(new PacmanUpTile());
            Assert.Equal(tile.TileType.Display, maze.MazeArray[level.Pacman.X, level.Pacman.Y].TileType.Display);
        }
        
        [Theory]
        [InlineData(Direction.Up, 1, 1 )]
        public void SpriteShouldUpdatePositionGivenCurrentDirection(Direction newDirection, int newX, int newY )
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(newDirection)).Returns(newDirection);
            
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[0]);
            var maze = new Maze(mazeData, SetUp());

            var tileTypeFactory = SetUp();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));            
            maze.MazeArray[0, 1].TileType = new WallTile();
            
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( tileTypeFactory, level.Pacman, maze);
           
            Assert.Equal(newX, level.Pacman.X);
            Assert.Equal(newY, level.Pacman.Y);
        }

    }
}