namespace Pacman.Interfaces
{
    public interface ITileTypeFactory
    {
        public ITileType Wall { get; }
        public ITileType Empty { get; }
        public ITileType Pellet { get; }
        void DisplayTile(ITile mazeMaze);
    }
}