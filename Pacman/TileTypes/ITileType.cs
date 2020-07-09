using System;

namespace Pacman.TileTypes
{
    public interface ITileType
    {
        string Display { get; }
        public ConsoleColor TileColour { get; }
        
        public bool HasBeenEaten { get; set; }
    }
}