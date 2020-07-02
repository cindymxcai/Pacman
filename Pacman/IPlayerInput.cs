using System;
using Pacman.Enums;

namespace Pacman
{
    public interface IPlayerInput
    {
        Direction TakeInput(Direction currentDirection, ConsoleKey input);
        bool IsStillPlaying();
    }
}