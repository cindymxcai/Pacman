using Pacman.Enums;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public interface ISpriteBehaviour
    { 
        Direction ChooseDirection();

        ITileType UpdateTileType(Direction direction);
    }
}