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
        private static ITileTypeFactory SetUpLevel()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile);
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
            var maze = new Maze(mazeData, SetUpLevel());

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            maze.MazeArray[1, 2].TileType = new WallTile();
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition(tileTypeFactory, level.Pacman, maze);
           
            Assert.Equal(0, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y); 
        }
        

        [Fact]
        public void SetNewPositionShouldUpdateOldPositionToo()
        {
            var pacman = new Sprite(3,0, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()));
            pacman.SetNewPosition(1,2);
            Assert.Equal(1,pacman.X);
            Assert.Equal(2,pacman.Y);
            Assert.Equal(3,pacman.PrevX);
            Assert.Equal(0,pacman.PrevY);
        }

        [Fact]
        public void UpdateFacingDirectionChangesPacmansCurrentPosition()
        {
            var pacman = new Sprite(0,0, new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()));
            pacman.UpdateCurrentDirection(Direction.Up);
            Assert.Equal(Direction.Up, pacman.CurrentDirection);
        }
    }
} 