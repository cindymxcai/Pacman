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
    public class GameValidatorTests
    {
        private ITileTypeFactory SetUp()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile);
        }
        
        private Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            return new Maze(mazeData, SetUp());
        }
        
        [Fact]
        public void WallCollisionIsTrueIfSpritessNextPositionIsAWall()
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(Direction.Right)).Returns(Direction.Right);
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            var maze = new Maze(mazeData, SetUp());
            var pacman = new Sprite(1,2, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()));
            var gameLogicValidator = new GameLogicValidator();
            maze.MazeArray[1, 2].TileType = new WallTile();
            Assert.True(gameLogicValidator.HasCollidedWithWall(SetUp(), (pacman.X, pacman.Y), maze));
        }
        
        [Fact]
        public void GhostCollisionIsTrueIfGhostAndPacmanAreOnSameTile()
        {
            var maze = MazeSetUp();
            var tileTypeFactory = SetUp();
            var level = new Level( tileTypeFactory, maze, new Display(), new SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(),
                new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile())) {Pacman = {X = 3, Y = 3}};
            level.Ghosts[0].X = 3;
            level.Ghosts[0].Y = 3;
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var maze = MazeSetUp();
            var tileTypeFactory = SetUp(); 
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            Assert.True(maze.HasEatenAllPellets(maze.Pellets));
            Assert.False(maze.HasEatenAllPellets(2));
        }
    }
}