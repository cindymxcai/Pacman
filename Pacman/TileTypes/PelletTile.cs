using System;

namespace Pacman.TileTypes
{
    public class PelletTile : ITileType
    {
        public string Display { get; } = " \u2022 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Magenta;
    }
}