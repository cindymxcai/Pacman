using System.Collections.Generic;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman.Interfaces
{
    public interface IGameLogicValidator
    {
        bool HasCollidedWithWall(ITileType tileType, (int x, int y) newPosition, IMaze gameMaze);
        bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites);
        bool HasEatenAllPellets(int remainingPellets);
    }
}