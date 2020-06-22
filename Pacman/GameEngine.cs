using System;
using System.Collections.Generic;
using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public static class GameEngine
    {
        public static void UpdateMazeTileDisplays(int counter, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts)
        {
            Display.UpdatePacmanDisplay(counter, gameMaze, pacman, pacman.CurrentDirection);
            gameMaze.UpdateMazeArray(pacman.PrevX, pacman.PrevY, TileType.Empty);
            foreach (var ghostSprite in ghosts)
            {
                var prevTileType = gameMaze.MazeArray[ghostSprite.PrevX, ghostSprite.PrevY].HasBeenEaten
                    ? TileType.Empty
                    : TileType.Pellet;
                
                gameMaze.UpdateMazeArray(ghostSprite.PrevX, ghostSprite.PrevY, prevTileType);
                gameMaze.UpdateMazeArray(ghostSprite.X, ghostSprite.Y, TileType.Ghost);
            }
        }

        public static void UpdateSpritePosition(ISprite sprite, IMaze gameMaze)
        {
            var (x, y) = GetNewPosition(sprite, gameMaze);
            if (!GameLogic.HasCollidedWithWall((x, y), gameMaze)) 
                sprite.SetNewPosition(x, y);
        }

        public static (int, int) GetNewPosition(ISprite sprite, IMaze gameMaze)
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
    }
}