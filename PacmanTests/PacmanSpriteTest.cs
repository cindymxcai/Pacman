﻿using System;
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
    public class PacmanSpriteTest
    {
        private ITileTypeFactory SetUpLevel()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            var pacmanChompTile = new PacmanChompTile();
            var pacmanUpTile = new PacmanUpTile();
            var pacmanDownTile = new PacmanDownTile();
            var pacmanLeftTile = new PacmanLeftTile();
            var pacmanRightTile = new PacmanRightTile();
            var ghostTile = new GhostTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile, pacmanChompTile, pacmanUpTile, pacmanDownTile, pacmanLeftTile, pacmanRightTile, ghostTile);
        }
        
        [Theory]
        [InlineData(Direction.Right, 1, 2 )]
        public void ShouldContinueToMoveInCurrentDirection(Direction newDirection, int newX, int newY )
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(newDirection)).Returns(newDirection);
            
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[0]);
            var maze = new Maze(mazeData);
            var tileFactory = new TileFactory();

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());            
            maze.MazeArray[1, 2].TileType = new PelletTile();
            
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( new WallTile(), level.Pacman, maze, level.GameLogicValidator);
           
            Assert.Equal(newX, level.Pacman.X);
            Assert.Equal(newY, level.Pacman.Y);
        }

        [Fact]
        public void ShouldNotMoveInCurrentDirectionIfNextPositionIsAWall()
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(Direction.Right)).Returns(Direction.Right);
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            var maze = new Maze(mazeData);
            var tileFactory = new TileFactory();

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());
            maze.MazeArray[1, 2].TileType = new WallTile();
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( maze.MazeArray[1,2].TileType, level.Pacman, maze, level.GameLogicValidator);
           
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y); 
        }


        [Fact]
        public void WallCollisionIsTrueIfPacmansNextTileIsAWall()
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(Direction.Right)).Returns(Direction.Right);
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            var maze = new Maze(mazeData);
            var pacman = new Sprite(1,2, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()));
            var gameLogicValidator = new GameLogicValidator();
            maze.MazeArray[1, 2].TileType = new WallTile();
            Assert.True(gameLogicValidator.HasCollidedWithWall(maze.MazeArray[1,2].TileType, (pacman.X, pacman.Y), maze));
        }

        [Fact]
        public void SetNewPositionShouldUpdateOldPositionToo()
        {
            var pacman = new Sprite(3,0, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()));
            pacman.SetNewPosition(1,2);
            Assert.Equal(1,pacman.X);
            Assert.Equal(2,pacman.Y);
            Assert.Equal(3,pacman.PrevX);
            Assert.Equal(0,pacman.PrevY);
        }

        [Fact]
        public void UpdateFacingDirectionChangesPacmansCurrentPosition()
        {
            var pacman = new Sprite(0,0, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()));
            pacman.UpdateCurrentDirection(Direction.Up);
            Assert.Equal(Direction.Up, pacman.CurrentDirection);
        }
    }
} 