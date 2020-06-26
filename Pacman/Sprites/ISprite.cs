using Pacman.Enums;

namespace Pacman.Sprites
{
    public interface ISprite
    {
        int X { get; }
        int Y { get; }
        int PrevX { get; }
        int PrevY { get; }
        Direction CurrentDirection { get; set; }
        void SetNewPosition(int x, int y);
        
        ISpriteBehaviour Behaviour { get; }

        void UpdateCurrentDirection(Direction newDirection);

    }
}