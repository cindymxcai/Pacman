using System;
using Pacman.Enums;
using Pacman.Interfaces;

namespace Pacman
{
    public class PlayerInput : IPlayerInput
    {
        public Direction TakeInput(Direction currentDirection) {
            var input = Console.ReadKey().Key; 
                return input switch {
                    ConsoleKey.RightArrow => Direction.Right,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                _ => currentDirection
                };
        }

        public bool HasPressedQuit()
        {
            return Console.ReadKey().Key == ConsoleKey.Q;
        }

        public bool HasNewInput()
        {
            return Console.KeyAvailable;
        }
    }
}