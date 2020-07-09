using Pacman.Enums;

namespace Pacman.Interfaces
{
    public interface IPlayerInput
    {
        Direction TakeInput(Direction currentDirection);
        bool HasPressedQuit();
        bool HasNewInput();
    }
}