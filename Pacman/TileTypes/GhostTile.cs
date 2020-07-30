using System;
using Pacman.Interfaces;

namespace Pacman.TileTypes
{
    public class GhostTile : ITileType
    {
        public string Display { get; } = " \u1571 ";
       
        public ConsoleColor TileColour { get; } = ConsoleColor.Red;
    }
}