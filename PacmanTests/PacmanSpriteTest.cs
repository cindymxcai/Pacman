﻿using System;
 using System.IO;
 using Moq;
 using Newtonsoft.Json;
 using Pacman;
using Pacman.Enums;
using Pacman.Sprites;
using Xunit;

namespace PacmanTests
{
    public class PacmanSpriteTest
    {
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
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            var maze = new Maze(mazeData);
            
            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new Display()), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( level.Pacman, maze, level.GameLogicValidator);
           
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
            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine( new Display()), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());

            maze.MazeArray[1, 2].TileType = TileType.Wall;
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( level.Pacman, maze, level.GameLogicValidator);
           
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y); 
        }

        [Fact]
        public void SetNewPositionShouldUpdateOldPositionToo()
        {
            var pacman = new Sprite(3,0, new PacmanBehaviour());
            pacman.SetNewPosition(1,2);
            Assert.Equal(1,pacman.X);
            Assert.Equal(2,pacman.Y);
            Assert.Equal(3,pacman.PrevX);
            Assert.Equal(0,pacman.PrevY);
        }

        [Fact]
        public void UpdateFacingDirectionChangesPacmansCurrentPosition()
        {
            var pacman = new Sprite(0,0, new PacmanBehaviour());
            pacman.UpdateCurrentDirection(Direction.Up);
            Assert.Equal(Direction.Up, pacman.CurrentDirection);
        }
    }
}