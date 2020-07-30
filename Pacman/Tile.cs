using Pacman.Interfaces;

namespace Pacman
{
    public class Tile : ITile
    {
        public ITileType TileType { get; set; }
        
        public bool HasBeenEaten { get; set; }

        public Tile(ITileType tileType)
        {
            TileType = tileType;
        }
    }
}