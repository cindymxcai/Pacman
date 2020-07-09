using Pacman.Enums;
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

        public void UpdatePacmanDisplay(ITileType pacmanUp, ITileType pacmanDown, ITileType pacmanLeft, ITileType pacmanRight,
            ITileType pacmanChomp, in bool isChomping, IMaze gameMaze, ISprite pacman, Direction pacmanCurrentDirection)
        {
            if (!isChomping)
            {
                switch (pacmanCurrentDirection)
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
            }        }
        
    }
}