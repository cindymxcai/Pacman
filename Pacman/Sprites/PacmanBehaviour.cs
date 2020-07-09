using Pacman.Enums;
using Pacman.Interfaces;
using Pacman.TileTypes;

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