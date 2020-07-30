namespace Pacman.Interfaces
{
    public interface ITile
    {
        ITileType TileType { get; set; }
        bool HasBeenEaten { get; set; }
    }
}