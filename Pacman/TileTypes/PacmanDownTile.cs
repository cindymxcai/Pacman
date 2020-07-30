using System;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class PacmanDownTile : ITileType
    {
        public string Display { get; } = " \u15E3 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow;
    }
}