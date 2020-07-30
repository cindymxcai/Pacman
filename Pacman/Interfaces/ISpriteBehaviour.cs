using Pacman.Enums;

namespace Pacman.Interfaces
{
    public interface ISpriteBehaviour
    { 
        Direction GetNewDirection( Direction direction);
        ITileType SetDisplayTile(Direction direction);
    }
}