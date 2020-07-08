using System.Collections.Generic;
using Pacman.Sprites;

namespace Pacman
{
    public interface IGameLogicValidator
    {
        bool HasCollidedWithWall((int x, int y) newPosition, IMaze gameMaze);
        bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites);
        bool HasEatenAllPellets(int remainingPellets);
    }
}