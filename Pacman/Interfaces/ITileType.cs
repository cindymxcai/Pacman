using System;

namespace Pacman.TileTypes
{
    public interface ITileType
    {
        string Display { get; set; }
        ConsoleColor TileColour { get; }
    }
}