using System;

namespace Pacman.TileTypes
{
    public class PacmanLeftTile : ITileType
    {
        public string Display { get; } = " \u15E4 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}