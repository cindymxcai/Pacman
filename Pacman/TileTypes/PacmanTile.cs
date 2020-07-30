using System;
using Pacman.Enums;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class PacmanTile : ITileType
    {
        public string Display { get; set; }
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;

        public void SetTileDisplay(bool isChomping, Direction direction)
        {
            if (isChomping) Display = " \u25EF ";
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        Display = " \u15E2 ";
                        break;
                    case Direction.Down:
                        Display = " \u15E3 ";
                        break;
                    case Direction.Left:
                        Display = " \u15E4 ";
                        break;
                    case Direction.Right:
                        Display = " \u15E7 ";
                        break;
                }
            }
        }
    }
}