using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public class Level
    {
        public IPlayerInput PlayerInput;
        public IPacmanSprite Pacman;
        public readonly List<IGhostSprite> Ghosts = new List<IGhostSprite>();
        public IMaze GameMaze { get; }
        public int LivesLeft { get; set; }
        public bool HasWon { get; set; }
        public int Score { get; private set; }

        public IGameEngine GameEngine { get; }
        
        public IGameLogicValidator GameLogicValidator { get; }

        public Level(int level, IFileReader fileReader)
        {
            GameLogicValidator = new GameLogicValidator();
            GameEngine = new GameEngine();
            LivesLeft = 3;
            PlayerInput = new PlayerInput();
            GameMaze = new Maze(level, fileReader);
            Pacman = new PacmanSprite(1, 1, Direction.Right);
            Ghosts.Add(new GhostSprite(9, 9, new RandomGhostBehaviour()));
            Ghosts.Add(new GhostSprite(9, 10, new RandomGhostBehaviour()));
        }

        public bool HasWonLevel()
        {
            var counter = 0;
            while (!HasWon)
            {
                var input = Console.ReadKey().Key;
                var newDirection = PlayerInput.TakeInput(Pacman.CurrentDirection, input);
                while (!Console.KeyAvailable)
                {
                    var remainingPellets =
                        GameMaze.MazeArray.Cast<Tile>().Count(tile => tile.TileType == TileType.Pellet);
                    
                    Score = Constants.GetScore(GameMaze.Pellets, remainingPellets);
                    
                    UpdateSpritePositions(newDirection);
                    GameEngine.UpdateMazeTileDisplays(counter, GameMaze, Pacman, Ghosts);
                    
                    if (GameLogicValidator.HasCollidedWithGhost(Pacman, Ghosts)) HandleDeath();
                    if (LivesLeft != 0)
                    {
                        HasWon = GameLogicValidator.HasEatenAllPellets(remainingPellets);
                        Console.Clear();
                        
                        Display.MazeOutput(GameMaze);
                        Display.GameStats(Score, LivesLeft);
                        
                        counter++;
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.2));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void HandleDeath()
        {
            LivesLeft--;
            Display.LostLife(LivesLeft);
            GameMaze.UpdateMazeArray(Pacman.X, Pacman.Y,
                GameMaze.MazeArray[Pacman.X, Pacman.Y].HasBeenEaten ? TileType.Empty : TileType.Pellet);
            
            Pacman = new PacmanSprite(1, 1, Direction.Right);
        }

        private void UpdateSpritePositions(Direction newDirection)
        {
            Pacman.UpdateFacingDirection(newDirection);
            GameEngine.UpdateSpritePosition(Pacman, GameMaze, GameLogicValidator);
            foreach (var ghostSprite in Ghosts)
            {
                ghostSprite.CurrentDirection = ghostSprite.Behaviour.ChooseDirection();
                GameEngine.UpdateSpritePosition(ghostSprite, GameMaze, GameLogicValidator);
            }
        }
    }
}