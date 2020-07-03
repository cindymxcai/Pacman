using System;
using Pacman.Enums;

namespace Pacman
{
    public interface IPlayerInput
    {
        Direction TakeInput(Direction currentDirection);
        bool isPressedQuit();
        bool HasNewInput();
    }
}