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
        private ITileTypeFactory SetUpLevel()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            var ghostTile = new GhostTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile,  ghostTile);
        }
        
        private Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            return new Maze(mazeData);
        }

        [Fact]
        public void UpdatesMazeWithCorrectTile()
        {
            var maze = MazeSetUp();

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileTypeFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( new PelletTile(),  level.Pacman, maze, level.GameLogicValidator);
            level.GameEngine.UpdateMazeTileDisplays(tileTypeFactory, maze, level.Pacman, level.Ghosts);

            Assert.Equal(new EmptyTile().Display, maze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].TileType.Display);
            var tile = new Tile(new PacmanRightTile());
            Assert.Equal(tile.TileType.Display, maze.MazeArray[level.Pacman.X, level.Pacman.Y].TileType.Display);
        }
        
        [Theory]
        [InlineData(Direction.Right, 1, 2 )]
        public void SpriteShouldUpatePositionGivenCurrentDirection(Direction newDirection, int newX, int newY )
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(newDirection)).Returns(newDirection);
            
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[0]);
            var maze = new Maze(mazeData);

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileTypeFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));            
            maze.MazeArray[1, 2].TileType = new PelletTile();
            
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( new WallTile(), level.Pacman, maze, level.GameLogicValidator);
           
            Assert.Equal(newX, level.Pacman.X);
            Assert.Equal(newY, level.Pacman.Y);
        }

    }
}