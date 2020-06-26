using System.Collections.Generic;
using Pacman.Sprites;

namespace Pacman
{
    public interface IGameLogicValidator
    {
        bool HasCollidedWithWall((int x, int y) newPosition, IMaze gameMaze);
        bool HasCollidedWithGhost(IPacmanSprite pacmanSprite, IEnumerable<IGhostSprite> ghostSprites);
        bool HasEatenAllPellets(int remainingPellets);
    }
}