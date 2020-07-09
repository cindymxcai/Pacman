using System;

namespace Pacman.TileTypes
{
    public class PacmanRightTile : ITileType
    {
        public string Display { get; set; } = " \u15E7 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}