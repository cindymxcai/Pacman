using System.Collections.Generic;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public class SpriteDisplay : ISpriteDisplay
    {
        private readonly IDictionary<Direction, ITileType> _directionMap =
            new Dictionary<Direction, ITileType>() //TODO HOW TO AVOID NEWING UP? AND CONSOLIDATE INTO ONE CLASS
            {
                {Direction.Up, new PacmanUpTile()},
                {Direction.Down, new PacmanDownTile()},
                {Direction.Left, new PacmanLeftTile()},
                {Direction.Right, new PacmanRightTile()}
            };

        public void UpdatePacmanDisplay(ITileTypeFactory tileTypeFactory, bool isChomping, IMaze gameMaze,
            ISprite pacman,
            Direction pacmanCurrentDirection)
        {
            gameMaze.MazeArray[pacman.X, pacman.Y].TileType =
                !isChomping ? _directionMap[pacmanCurrentDirection] : tileTypeFactory.Chomp;
        }
    }
}