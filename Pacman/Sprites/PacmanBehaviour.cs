using System;
using Pacman.Enums;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public class PacmanBehaviour : ISpriteBehaviour
    {
        private readonly PacmanUpTile _pacmanUpTile;
        private readonly PacmanDownTile _pacmanDownTile;
        private readonly PacmanLeftTile _pacmanLeftTile;
        private readonly PacmanRightTile _pacmanRightTile;

        public PacmanBehaviour(PacmanUpTile pacmanUpTile, PacmanDownTile pacmanDownTile, PacmanLeftTile pacmanLeftTile, PacmanRightTile pacmanRightTile)
        {
            _pacmanUpTile = pacmanUpTile;
            _pacmanDownTile = pacmanDownTile;
            _pacmanLeftTile = pacmanLeftTile;
            _pacmanRightTile = pacmanRightTile;
        }

        public bool IsChomping { get; set; }

        public Direction ChooseDirection()
        {
            return Direction.Right;
        }

        public ITileType SetTileType(Direction direction)
        {
            if (IsChomping)
            {
                return new PacmanChompTile();
            }

            return direction switch
            {
                Direction.Up =>  _pacmanUpTile,
                Direction.Down => _pacmanDownTile,
                Direction.Left => _pacmanLeftTile,
                Direction.Right => _pacmanRightTile
            };

        }

    }
}