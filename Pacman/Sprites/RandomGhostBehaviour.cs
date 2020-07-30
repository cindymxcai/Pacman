using Pacman.Enums;
using Pacman.Interfaces;

namespace Pacman.Sprites
{
    public class RandomGhostBehaviour : ISpriteBehaviour
    {
        private readonly ITileType _ghostTile;
        public IRng Rng;
        public RandomGhostBehaviour(ITileType ghostTile)
        {
            _ghostTile = ghostTile;
            Rng = new Rng();
        }
        
        public Direction GetNewDirection(Direction newDirection)
        {
             newDirection = Rng.Next(0, 4) switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
                _ => newDirection
            };
            return newDirection;
        }

        public ITileType SetDisplayTile(Direction direction)
        {
            return _ghostTile;
        }
    }
}