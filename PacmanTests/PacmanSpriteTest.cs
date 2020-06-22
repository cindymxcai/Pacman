﻿using System;
using Moq;
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
            mockInput.Setup(input => input.TakeInput(newDirection, ConsoleKey.RightArrow)).Returns(newDirection);
            var level = new Level(1, new FileReader()){PlayerInput = mockInput.Object};
            GameEngine.GetNewPosition(level.Pacman, level.GameMaze);
            GameEngine.UpdateSpritePosition( level.Pacman, level.GameMaze);
           
            Assert.Equal(newX, level.Pacman.X);
            Assert.Equal(newY, level.Pacman.Y);
        }

        [Fact]
        public void ShouldNotMoveInCurrentDirectionIfNextPositionIsAWall()
        {
            var mockInput = new Mock<IPlayerInput>();
            mockInput.Setup(input => input.TakeInput(Direction.Right, ConsoleKey.RightArrow)).Returns(Direction.Right);
            var level = new Level(1, new FileReader()){PlayerInput = mockInput.Object};
            level.GameMaze.MazeArray[1, 2].TileType = TileType.Wall;
            GameEngine.GetNewPosition(level.Pacman, level.GameMaze);
            GameEngine.UpdateSpritePosition( level.Pacman, level.GameMaze);
           
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y); 
        }

        [Fact]
        public void SetNewPositionShouldUpdateOldPositionToo()
        {
            var pacman = new PacmanSprite(3,0, Direction.Down);
            pacman.SetNewPosition(1,2);
            Assert.Equal(1,pacman.X);
            Assert.Equal(2,pacman.Y);
            Assert.Equal(3,pacman.PrevX);
            Assert.Equal(0,pacman.PrevY);
        }

        [Fact]
        public void UpdateFacingDirectionChangesPacmansCurrentPosition()
        {
            var pacman = new PacmanSprite(0,0, Direction.Down);
            pacman.UpdateFacingDirection(Direction.Up);
            Assert.Equal(Direction.Up, pacman.CurrentDirection);
        }
    }
}