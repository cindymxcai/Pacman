using System;

namespace Pacman.TileTypes
{
    public class WallTile : ITileType
    {
        public string Display { get; set; } = "\u2588\u2588\u2588";
        public ConsoleColor TileColour { get; } = ConsoleColor.Blue;
    }
}