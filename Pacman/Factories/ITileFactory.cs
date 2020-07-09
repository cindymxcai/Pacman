using System;
using Pacman.Interfaces;

namespace Pacman.Factories
{
    public interface ITileFactory
    {
        void DisplayTile(ITile tile);
    }

    public class TileFactory : ITileFactory
    {
        public void DisplayTile(ITile tile)
        {
            Console.ForegroundColor = tile.TileType.TileColour;
            Console.Write(tile.TileType.Display);
            Console.ResetColor();        
        }
    }
}
