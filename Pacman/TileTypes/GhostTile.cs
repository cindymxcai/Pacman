using System;

namespace Pacman.TileTypes
{
    public class GhostTile : ITileType
    {
        public string Display { get; set; } = " \u1571 ";
       
        public ConsoleColor TileColour { get; } = ConsoleColor.Red;
    }
}