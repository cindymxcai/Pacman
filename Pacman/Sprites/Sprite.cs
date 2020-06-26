using Pacman.Enums;

namespace Pacman.Sprites
{
    public class Sprite : ISprite
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }
        public Direction CurrentDirection { get; set; }
        public ISpriteBehaviour Behaviour { get; }
        
        public Sprite(int x, int y, ISpriteBehaviour spriteBehaviour)
        {
            X = x;
            Y = y;
            Behaviour = spriteBehaviour;
            CurrentDirection = Behaviour.ChooseDirection();
        }
        public void SetNewPosition(int x, int y)
        {
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }
        public void UpdateCurrentDirection(Direction newDirection)
        {
            CurrentDirection = newDirection;
        }
    }
}