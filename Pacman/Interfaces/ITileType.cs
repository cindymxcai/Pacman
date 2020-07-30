using System;

namespace Pacman.Interfaces
{
    public interface ITileType
    {
        string Display { get; set; }
        ConsoleColor TileColour { get; }
    }
}