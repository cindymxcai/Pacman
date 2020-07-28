using System;
using System.Collections.Generic;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman
{
    public class GameEngine : IGameEngine
    {
        /// <summary>
        /// This class performs "updates" within the game as the game ticks. Based on logic checks performed by
        /// <c>GameLogicValidator</c>, this class will change the state of game objects
        /// </summary>
        public void UpdateMazeTileDisplays(ITileTypeFactory tileTypeFactory, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts)
        {
            pacman.UpdateDisplay();
            gameMaze.UpdateMazeArray(pacman.X, pacman.Y, pacman.SpriteDisplay, tileTypeFactory.Empty );
            gameMaze.UpdateMazeArray(pacman.PrevX, pacman.PrevY, tileTypeFactory.Empty, tileTypeFactory.Empty);
            foreach (var ghostSprite in ghosts)
            {
                var prevTileType = gameMaze.MazeArray[ghostSprite.PrevX, ghostSprite.PrevY].HasBeenEaten
                    ? tileTypeFactory.Empty
                    : tileTypeFactory.Pellet;
                
                gameMaze.UpdateMazeArray(ghostSprite.PrevX, ghostSprite.PrevY, prevTileType, tileTypeFactory.Empty);
                gameMaze.UpdateMazeArray(ghostSprite.X, ghostSprite.Y, ghostSprite.SpriteDisplay, tileTypeFactory.Empty); 
            }
        }

        public void UpdateSpritePosition(ITileTypeFactory tileTypeFactory, ISprite sprite, IMaze gameMaze, IGameLogicValidator gameLogicValidator)
        {
            var (x, y) = GetNewPosition(sprite, gameMaze);
            if (!gameLogicValidator.HasCollidedWithWall(tileTypeFactory, (x, y), gameMaze)) 
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
    }
}