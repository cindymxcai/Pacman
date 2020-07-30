using System;
using Pacman.Enums;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public class PacmanBehaviour : ISpriteBehaviour
    {
        private readonly PacmanTile _pacmanTile;

        public PacmanBehaviour(PacmanTile pacmanTile)
        {
            _pacmanTile = pacmanTile;
        }

        private bool IsChomping { get; set; }

        public Direction GetNewDirection(Direction direction)
        {
            return direction;
        }

        public ITileType SetDisplayTile(Direction direction)
        {
            UpdateChompingState();

            _pacmanTile.SetTileDisplay(IsChomping, direction);

            return _pacmanTile;
        }

        private void UpdateChompingState()
        {
            IsChomping = !IsChomping;
        }
    }
}