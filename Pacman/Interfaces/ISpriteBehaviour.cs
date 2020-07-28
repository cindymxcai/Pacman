using Pacman.Enums;
using Pacman.TileTypes;

namespace Pacman.Interfaces
{
    public interface ISpriteBehaviour
    { 
        Direction ChooseDirection( Direction direction);

        ITileType UpdateTileType(Direction direction);
    }
}