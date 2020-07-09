using System;

namespace Pacman.TileTypes
{
    public class PacmanTile : ITileType
    {
        public string Display { get; set; } = " \u25EF ";

        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}