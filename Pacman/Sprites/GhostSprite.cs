using Pacman.Enums;

namespace Pacman.Sprites
{
    public class GhostSprite : IGhostSprite
    {
        public IGhostBehaviour Behaviour { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int PrevY { get; private set; }
        public int PrevX { get; private set; }
        public Direction CurrentDirection { get; set; }

        public GhostSprite(int x, int y, IGhostBehaviour behaviour)
        {
            Behaviour = behaviour;
            X = x;
            Y = y;
            CurrentDirection = Behaviour.ChooseDirection();
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