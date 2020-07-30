using System;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class EmptyTile : ITileType
    {
        public string Display { get; } = "   ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Black;
    }
}