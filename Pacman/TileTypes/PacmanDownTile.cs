using System;

namespace Pacman.TileTypes
{
    public class PacmanDownTile : ITileType
    {
        public string Display { get; set; } = " \u15E3 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}