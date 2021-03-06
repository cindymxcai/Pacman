namespace Pacman.Interfaces
{
    public interface IMaze
    {
        ITile[,] MazeArray { get; }
        int Height { get; }
        int Width { get; }
        void UpdateMazeArray(int x, int y, ITileType tileType, ITileType empty);
        void Render();
        bool HasEatenAllPellets(int pelletsEaten);
    }
}