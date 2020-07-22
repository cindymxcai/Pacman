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
        private readonly PacmanChompTile _pacmanChompTile;

        public PacmanBehaviour(PacmanUpTile pacmanUpTile, PacmanDownTile pacmanDownTile, PacmanLeftTile pacmanLeftTile, PacmanRightTile pacmanRightTile, PacmanChompTile pacmanChompTile)
        {
            _pacmanUpTile = pacmanUpTile;
            _pacmanDownTile = pacmanDownTile;
            _pacmanLeftTile = pacmanLeftTile;
            _pacmanRightTile = pacmanRightTile;
            _pacmanChompTile = pacmanChompTile;
        }

        private bool IsChomping { get; set; }

        public Direction ChooseDirection()
        {
            return Direction.Right; 
        }

        public ITileType GetTileType(Direction direction)
        {

            IsChomping = !IsChomping;
            
            if (IsChomping)
            {
                return _pacmanChompTile;
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