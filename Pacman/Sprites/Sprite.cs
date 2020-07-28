using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public class Sprite : ISprite
    {
        public ITileType SpriteDisplay { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }
        public Direction CurrentDirection { get; set; }
        public ISpriteBehaviour Behaviour { get; }
        public Sprite(int x, int y, ISpriteBehaviour spriteBehaviour)
        {
            X = x;
            Y = y;
            Behaviour = spriteBehaviour;
            CurrentDirection = Behaviour.ChooseDirection(CurrentDirection);
            SpriteDisplay = Behaviour.UpdateTileType(CurrentDirection);
        }

        public void SetNewPosition(int x, int y)
        {
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }

        public void UpdateCurrentDirection(Direction direction)
        {
            CurrentDirection = Behaviour.ChooseDirection(direction);
        }

        public void UpdateDisplay()
        {
            SpriteDisplay = Behaviour.UpdateTileType(CurrentDirection);
        }
    }
}