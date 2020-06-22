using Pacman.Enums;

namespace Pacman
{
    public interface IMaze
    {
        Tile[,] MazeArray { get; }
        int Pellets { get; }
        int Height { get; }
        int Width { get; }
        void UpdateMazeArray(int x, int y, TileType tileType);
    }
}