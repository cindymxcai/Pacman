using System;
using System.Collections.Generic;
using Pacman.Enums;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman
{
    public class GameEngine : IGameEngine
    {
        public void UpdateMazeTileDisplays(ITileType ghost, ITileType pacmanUp, ITileType pacmanDown, ITileType pacmanLeft, ITileType pacmanRight, ITileType pacmanChomp,ITileType empty, ITileType pellet, bool isChomping, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts)
        {
            pacman.UpdatePacmanDisplay(pacmanUp, pacmanDown, pacmanLeft, pacmanRight, pacmanChomp,isChomping, gameMaze, pacman, pacman.CurrentDirection);
            gameMaze.UpdateMazeArray(pacman.PrevX, pacman.PrevY, empty);
            foreach (var ghostSprite in ghosts)
            {
                var prevTileType = gameMaze.MazeArray[ghostSprite.PrevX, ghostSprite.PrevY].HasBeenEaten
                    ? empty
                    : pellet;
                
                gameMaze.UpdateMazeArray(ghostSprite.PrevX, ghostSprite.PrevY, prevTileType);
                gameMaze.UpdateMazeArray(ghostSprite.X, ghostSprite.Y, ghost); 
            }
        }

        public void UpdateSpritePosition(ITileType wall, ISprite sprite, IMaze gameMaze, IGameLogicValidator gameLogicValidator)
        {
            var (x, y) = GetNewPosition(sprite, gameMaze);
            if (!gameLogicValidator.HasCollidedWithWall(wall, (x, y), gameMaze)) 
                sprite.SetNewPosition(x, y);
        }

        public (int, int) GetNewPosition(ISprite sprite, IMaze gameMaze)
        {
            return sprite.CurrentDirection switch
            {
                Direction.Up when sprite.X - 1 < 0 => (gameMaze.Height - 1, sprite.Y),
                Direction.Up => (sprite.X - 1, sprite.Y),
                Direction.Down when sprite.X + 1 > gameMaze.Height - 1 => (0, sprite.Y),
                Direction.Down => (sprite.X + 1, sprite.Y),
                Direction.Left when sprite.Y - 1 < 0 => (sprite.X, gameMaze.Width - 1),
                Direction.Left => (sprite.X, sprite.Y - 1),
                Direction.Right when sprite.Y + 1 > gameMaze.Width - 1 => (sprite.X, 0),
                Direction.Right => (sprite.X, sprite.Y + 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        //TODO: SHOULD NOT BE HERE
        private static void UpdatePacmanDisplay(ITileType pacmanUp, ITileType pacmanDown, ITileType pacmanLeft, ITileType pacmanRight, ITileType pacmanChomp,bool isChomping, IMaze gameMaze, ISprite pacman, Direction direction)
        {
            if (!isChomping)
            {
                switch (direction)
                {
                    case Direction.Up:
                        gameMaze.MazeArray[pacman.X, pacman.Y].TileType = pacmanUp;
                        break;
                    case Direction.Down:
                        gameMaze.MazeArray[pacman.X, pacman.Y].TileType = pacmanDown;
                        break;
                    case Direction.Left:
                        gameMaze.MazeArray[pacman.X, pacman.Y].TileType = pacmanLeft;
                        break;
                    case Direction.Right:
                        gameMaze.MazeArray[pacman.X, pacman.Y].TileType = pacmanRight;
                        break;
                    default:
                        gameMaze.MazeArray[pacman.X, pacman.Y] = gameMaze.MazeArray[pacman.X, pacman.Y];
                        break;
                }
            }
            else
            {
                gameMaze.MazeArray[pacman.X, pacman.Y].TileType = pacmanChomp;
            }
        }
    }
}