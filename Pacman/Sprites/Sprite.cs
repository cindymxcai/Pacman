using Pacman.Enums;
using Pacman.Interfaces;

namespace Pacman.Sprites
{
    public class Sprite : ISprite
    {
        public ITileType SpriteDisplay { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }
        public Direction CurrentDirection { get; private set; }
        private ISpriteBehaviour Behaviour { get; }
        public Sprite(int x, int y, ISpriteBehaviour spriteBehaviour)
        {
            X = x;
            Y = y;
            Behaviour = spriteBehaviour;
            CurrentDirection = Behaviour.GetNewDirection(CurrentDirection);
            SpriteDisplay = Behaviour.SetDisplayTile(CurrentDirection);
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
            CurrentDirection = Behaviour.GetNewDirection(direction);
        }

        public void UpdateDisplay()
        {
            SpriteDisplay = Behaviour.SetDisplayTile(CurrentDirection);
        }
    }
}