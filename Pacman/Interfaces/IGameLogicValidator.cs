using System.Collections.Generic;
using Pacman.Factories;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman.Interfaces
{
    public interface IGameLogicValidator
    {
        bool HasCollidedWithWall(ITileTypeFactory tileTypeFactory, (int x, int y) newPosition, IMaze gameMaze);
        bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites);
    }
}