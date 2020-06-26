using System.Collections.Generic;
using System.Linq;
using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public class GameLogicValidator : IGameLogicValidator
    {
        public bool HasCollidedWithWall((int x, int y) newPosition, IMaze gameMaze)
        {
            var (x, y) = newPosition;
            return gameMaze.MazeArray[x, y].TileType == TileType.Wall;
        }

        public bool HasCollidedWithGhost(IPacmanSprite pacmanSprite, IEnumerable<IGhostSprite> ghostSprites)
        {
            return ghostSprites.Any(ghost =>
                ghost.X == pacmanSprite.X && ghost.Y == pacmanSprite.Y || ghost.PrevX == pacmanSprite.X &&
                ghost.PrevY == pacmanSprite.Y && ghost.X == pacmanSprite.PrevX && ghost.Y == pacmanSprite.PrevY);
        }

        public bool HasEatenAllPellets(int remainingPellets)
        {
            return remainingPellets == 0;
        }
    }
}