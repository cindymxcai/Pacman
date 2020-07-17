using System;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.TileTypes;

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

        public Direction ChooseDirection()
        {
            var direction = Rng.Next(0, 4) switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
                _ => throw new Exception()
            };
            return direction;
        }

        public ITileType SetTileType(Direction direction)
        {
            return _ghostTile;
        }
    }
}