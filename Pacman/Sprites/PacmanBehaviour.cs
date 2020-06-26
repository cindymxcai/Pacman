using Pacman.Enums;

namespace Pacman.Sprites
{
    public class PacmanBehaviour : ISpriteBehaviour
    {
        public Direction ChooseDirection()
        {
            return Direction.Right;
        }
    }
}