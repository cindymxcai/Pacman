using Pacman.Enums;

namespace Pacman.Sprites
{
    public class PacmanSprite : IPacmanSprite
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }
        public Direction CurrentDirection { get; set; }

        public PacmanSprite(int x, int y, Direction startingDirection)
        {
            CurrentDirection = startingDirection;
            X = x;
            Y = y;
        }

        public void UpdateFacingDirection(Direction newDirection)
        {
            CurrentDirection = newDirection;
        }

        public void SetNewPosition(int x, int y)
        {
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }
    }
}