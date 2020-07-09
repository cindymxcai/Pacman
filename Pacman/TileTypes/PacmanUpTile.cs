using System;

namespace Pacman.TileTypes
{
    public class PacmanUpTile : ITileType
    {
        public string Display { get; } =  " \u15E2 ";
        public ConsoleColor TileColour { get; } = ConsoleColor.Yellow; 
        public bool HasBeenEaten { get; set; }
    }
}