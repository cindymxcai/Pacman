using Pacman.Enums;

namespace Pacman.Interfaces
{
    public interface ISprite
    {
        int X { get; set; }
        int Y { get; set; }
        int PrevX { get; }
        int PrevY { get; }
        Direction CurrentDirection { get;  }
        void SetNewPosition(int x, int y);
        ITileType SpriteDisplay { get; }
        void UpdateCurrentDirection(Direction direction);
        void UpdateDisplay();
    }
}