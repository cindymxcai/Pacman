using System;
using Pacman;
using Pacman.Enums;
using Xunit;

namespace PacmanTests
{
    public class PlayerInputTest
    {
        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        public void GivenConsoleKeyShouldReturnCorrectDirection(ConsoleKey consoleKey, Direction direction)
        {
            var playerInput = new PlayerInput();
            Assert.Equal(direction, playerInput.TakeInput(Direction.Left, consoleKey));
        }
    }
}