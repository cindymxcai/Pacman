using Pacman.TileTypes;

namespace Pacman.Factories
{
    public interface ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Chomp { get; }
        public ITileType Up { get; }
        public ITileType Down { get; }
        public ITileType Left { get; }
        public ITileType Right { get; }
        public ITileType Ghost { get; }
        
    }

    public class TileTypeFactory : ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Chomp { get; }
        public ITileType Up { get; }
        public ITileType Down { get; }
        public ITileType Left { get; }
        public ITileType Right { get; }
        public ITileType Ghost { get; }

        public TileTypeFactory(ITileType wall, ITileType empty, ITileType pellet, ITileType chomp, ITileType up, ITileType down, ITileType left, ITileType right, ITileType ghost)
        {
            Wall = wall;
            Empty = empty;
            Pellet = pellet;
            Chomp = chomp;
            Up = up;
            Down = down;
            Left = left;
            Right = right;
            Ghost = ghost;
        }
    }
}