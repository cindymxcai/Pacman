using System;
using System.Collections.Generic;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman.Sprites
{
    public class Sprite : ISprite
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PrevX { get; private set; }
        public int PrevY { get; private set; }
        public Direction CurrentDirection { get; set; }
        public ISpriteBehaviour Behaviour { get; }
        
        private readonly IDictionary<Direction, ITileType > _directionMap = new Dictionary<Direction, ITileType>() //TODO HOW TO AVOID NEWING UP?
        {
            {Direction.Up,  new PacmanUpTile()}, 
            {Direction.Down, new PacmanDownTile()},
            {Direction.Left, new PacmanLeftTile()},
            {Direction.Right, new PacmanRightTile()}
        };

        public Sprite(int x, int y, ISpriteBehaviour spriteBehaviour)
        {
            X = x;
            Y = y;
            Behaviour = spriteBehaviour;
            CurrentDirection = Behaviour.ChooseDirection();
        }
        public void SetNewPosition(int x, int y)
        {
            PrevX = X;
            PrevY = Y;
            X = x;
            Y = y;
        }
        public void UpdateCurrentDirection(Direction newDirection)
        {
            CurrentDirection = newDirection;
        }

        public void UpdatePacmanDisplay(ITileTypeFactory tileTypeFactory, bool isChomping, IMaze gameMaze, ISprite pacman, Direction pacmanCurrentDirection)
        {
            gameMaze.MazeArray[pacman.X, pacman.Y].TileType = !isChomping ? _directionMap[pacmanCurrentDirection] : tileTypeFactory.Chomp;
        }
    }
}