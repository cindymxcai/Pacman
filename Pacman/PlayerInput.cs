using System;
using Pacman.Enums;

namespace Pacman
{
    public class PlayerInput : IPlayerInput
    {
        public Direction TakeInput(Direction currentDirection, ConsoleKey input) {
            if (IsInputValid(input)) {
                return input switch {
                    ConsoleKey.RightArrow => Direction.Right,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    _ => throw new Exception()
                };
            }
            return currentDirection;
        }

        public bool IsStillPlaying()
        {
            return Console.ReadKey().Key != ConsoleKey.Q;
        }

        private static bool IsInputValid(ConsoleKey input)
        {
            return input == ConsoleKey.UpArrow || input == ConsoleKey.DownArrow || input == ConsoleKey.LeftArrow ||
                   input == ConsoleKey.RightArrow;
        }
    }
}