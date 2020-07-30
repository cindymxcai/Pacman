using System;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class PelletTile : ITileType
    {
        public string Display { get; set; } = " \u2022 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Magenta;
    }
}