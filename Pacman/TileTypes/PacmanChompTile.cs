using System;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class PacmanChompTile : ITileType
    {
        public string Display { get; } = " \u25EF ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}  