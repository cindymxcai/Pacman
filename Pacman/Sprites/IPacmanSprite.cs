using Pacman.Enums;

namespace Pacman.Sprites
{
    public interface IPacmanSprite : ISprite
    {
        void UpdateFacingDirection(Direction newDirection);
    }
}