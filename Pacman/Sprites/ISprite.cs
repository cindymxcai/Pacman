using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public interface ISprite
    {
        int X { get; set; }
        int Y { get; set; }
        int PrevX { get; }
        int PrevY { get; }
        Direction CurrentDirection { get; set; }
        void SetNewPosition(int x, int y);
        
        ISpriteBehaviour Behaviour { get; }

        void UpdateCurrentDirection(Direction newDirection);
        void UpdatePacmanDisplay(ITileTypeFactory tileTypeFactory, bool isChomping, IMaze gameMaze, ISprite pacman, Direction pacmanCurrentDirection);
    }
}