using System;

namespace Pacman.TileTypes
{
    public class EmptyTile : ITileType
    {
        public string Display { get; } = "   ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Black;
        public bool HasBeenEaten { get; set; } = true;
    }
} 