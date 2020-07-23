using System;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Factories
{
    public class TileTypeFactory : ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }

        public TileTypeFactory(ITileType wall, ITileType empty, ITileType pellet)
        {
            Wall = wall;
            Empty = empty;
            Pellet = pellet;
        }
        
        public void DisplayTile(ITile tile)
        {
            Console.ForegroundColor = tile.TileType.TileColour;
            Console.Write(tile.TileType.Display);
            Console.ResetColor();        
        }
    }
}