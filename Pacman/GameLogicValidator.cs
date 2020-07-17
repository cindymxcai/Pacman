using System.Collections.Generic;
using System.Linq;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman
{
    /// <summary>
    /// 
    /// </summary>
    public class GameLogicValidator : IGameLogicValidator
    {
        public bool HasCollidedWithWall(ITileType wall,(int x, int y) newPosition, IMaze gameMaze)
        {
            var (x, y) = newPosition;
            return gameMaze.MazeArray[x, y].TileType.Display == wall.Display; 
        }

        public bool HasCollidedWithGhost(ISprite pacmanSprite, IEnumerable<ISprite> ghostSprites)
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