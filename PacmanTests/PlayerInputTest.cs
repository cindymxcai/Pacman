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
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        public void GivenConsoleKeyShouldReturnCorrectDirection(ConsoleKey consoleKey, Direction direction)
        {
            var playerInput = new PlayerInput();
            Assert.Equal(direction, playerInput.TakeInput(Direction.Up, consoleKey));
        }
    }
}