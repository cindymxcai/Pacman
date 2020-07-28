using System.Collections.Generic;
using System.Linq;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman
{
    /// <summary>
    /// This class performs logic checks required by the game to then execute actions based on the result of these logic checks
    /// </summary>
    public class GameLogicValidator : IGameLogicValidator
    {
        public bool HasCollidedWithWall(ITileTypeFactory tileTypeFactory,(int x, int y) newPosition, IMaze gameMaze)
        {
            return gameMaze.MazeArray[newPosition.x, newPosition.y].TileType.Display == tileTypeFactory.Wall.Display;
        }

        public bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites)
        {
            return ghostSprites.Any(ghost =>
                ghost.X == pacmanSprite.X && ghost.Y == pacmanSprite.Y || ghost.PrevX == pacmanSprite.X &&
                ghost.PrevY == pacmanSprite.Y && ghost.X == pacmanSprite.PrevX && ghost.Y == pacmanSprite.PrevY);
        }
    }
}