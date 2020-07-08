using System;
using Pacman.Enums;

namespace Pacman
{
    public interface ITileFactory
    {
        void DisplayTile(Tile tile);
    }

    public class TileFactory : ITileFactory
    { 
        public void DisplayTile(Tile tile)
        {
            Console.ForegroundColor = tile.TileColour;
            Console.Write(tile.Display);
            Console.ResetColor();
        }
    }
}