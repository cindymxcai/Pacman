using Pacman.TileTypes;

namespace Pacman.Factories
{
    public interface ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Ghost { get; }
        
        
    }

    public class TileTypeFactory : ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Ghost { get; }

        public TileTypeFactory(ITileType wall, ITileType empty, ITileType pellet, ITileType ghost)
        {
            Wall = wall;
            Empty = empty;
            Pellet = pellet;
            Ghost = ghost;
        }
    }
}