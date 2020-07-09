using Pacman.Enums;
using Pacman.TileTypes;

namespace Pacman.Interfaces
{
    public interface IMaze
    {
        ITile[,] MazeArray { get; }
        int Pellets { get; }
        int Height { get; }
        int Width { get; }
        void UpdateMazeArray(int x, int y, ITileType tileType);
    }
}