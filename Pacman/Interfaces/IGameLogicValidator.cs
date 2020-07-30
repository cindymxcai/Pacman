using System.Collections.Generic;

namespace Pacman.Interfaces
{
    public interface IGameLogicValidator
    {
        bool HasCollidedWithWall(ITileTypeFactory tileTypeFactory, (int x, int y) newPosition, IMaze gameMaze);
        bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites);
    }
}