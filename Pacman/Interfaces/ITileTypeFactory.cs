using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Factories
{
    public interface ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        public ITileType Ghost { get; }
        void DisplayTile(ITile mazeMaze);
    }
}