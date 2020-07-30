using System;

namespace Pacman.Interfaces
{
    public interface ITileType
    {
        string Display { get;  }
        ConsoleColor TileColour { get; }
    }
}