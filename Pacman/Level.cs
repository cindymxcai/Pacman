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
        public ISprite Pacman;
        public readonly List<ISprite> Ghosts = new List<ISprite>();
        public IMaze GameMaze { get; }
        public int LivesLeft { get; set; }
        public bool HasWon { get; set; }
        public int Score { get; private set; }

        public IGameEngine GameEngine { get; }
        
        public IGameLogicValidator GameLogicValidator { get; }

        public Level(IMaze maze, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput)
        {
            GameLogicValidator = gameLogicValidator;
            GameEngine = gameEngine;
            LivesLeft = 3;
            PlayerInput = playerInput;
            GameMaze = maze;
            Pacman = new Sprite(1, 1, new PacmanBehaviour());
            Ghosts.Add(new Sprite(9, 9, new RandomGhostBehaviour()));
            Ghosts.Add(new Sprite(9, 10, new RandomGhostBehaviour()));
        }

        public void PlayLevel()
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
                    
                    if (GameLogicValidator.HasCollidedWithGhost(Pacman, Ghosts)) 
                        HandleDeath();
                    
                    if (LivesLeft == 0)
                        break;

                    HasWon = GameLogicValidator.HasEatenAllPellets(remainingPellets);
                    
                    Console.Clear();

                    Display.MazeOutput(GameMaze);
                    Display.GameStats(Score, LivesLeft);

                    counter++;
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.2));
                    
                }
                if (LivesLeft != 0) continue;
                break;
            }
        }

        public void HandleDeath()
        {
            LivesLeft--;
            Display.LostLife(LivesLeft);
            GameMaze.UpdateMazeArray(Pacman.X, Pacman.Y,
                GameMaze.MazeArray[Pacman.X, Pacman.Y].HasBeenEaten ? TileType.Empty : TileType.Pellet);
            
            Pacman = new Sprite(1, 1, new PacmanBehaviour());
        }

        private void UpdateSpritePositions(Direction newDirection)
        {
            Pacman.UpdateCurrentDirection(newDirection);
            GameEngine.UpdateSpritePosition(Pacman, GameMaze, GameLogicValidator);
            foreach (var ghostSprite in Ghosts)
            {
                ghostSprite.CurrentDirection = ghostSprite.Behaviour.ChooseDirection();
                GameEngine.UpdateSpritePosition(ghostSprite, GameMaze, GameLogicValidator);
            }
        }
    }
}