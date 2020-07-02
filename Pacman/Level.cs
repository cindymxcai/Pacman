using System;
using System.Collections.Generic;
using System.IO;
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
        public int LivesLeft { get; set; }
        public bool HasWon { get; set; }
        public int Score { get; private set; }
        public IGameEngine GameEngine { get; }
        public IGameLogicValidator GameLogicValidator { get; }

        public Level( ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour)
        {
            GameLogicValidator = gameLogicValidator;
            GameEngine = gameEngine;
            PlayerInput = playerInput;
            Pacman = spriteFactory.CreateSprite(1, 1, pacmanBehaviour);
            Ghosts.Add(spriteFactory.CreateSprite(9,9, ghostBehaviour));
            Ghosts.Add(spriteFactory.CreateSprite(9,10, ghostBehaviour));
        }

        public void PlayLevel(IMaze gameMaze)
        {
            HasWon = false;
            LivesLeft = 3;
            var counter = 0;
            while (!HasWon)
            {
                var input = Console.ReadKey().Key;
                var newDirection = PlayerInput.TakeInput(Pacman.CurrentDirection, input);
                while (!Console.KeyAvailable)
                {
                    var remainingPellets =
                        gameMaze.MazeArray.Cast<Tile>().Count(tile => tile.TileType == TileType.Pellet);
                    
                    Score = Constants.GetScore(gameMaze.Pellets, remainingPellets);
                    
                    UpdateSpritePositions(newDirection, gameMaze);
                    GameEngine.UpdateMazeTileDisplays(counter, gameMaze, Pacman, Ghosts);
                    
                    if (GameLogicValidator.HasCollidedWithGhost(Pacman, Ghosts)) 
                        HandleDeath(gameMaze);
                    
                    if (LivesLeft == 0)
                        break;

                    HasWon = GameLogicValidator.HasEatenAllPellets(remainingPellets);
                    
                    Console.Clear();

                    Display.MazeOutput(gameMaze);
                    Display.GameStats(Score, LivesLeft);

                    counter++;
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.2));
                    
                }
                if (LivesLeft != 0) continue;
                break;
            }
        }

        public void HandleDeath(IMaze gameMaze)
        {
            LivesLeft--;
            Display.LostLife(LivesLeft);
            gameMaze.UpdateMazeArray(Pacman.X, Pacman.Y,
                gameMaze.MazeArray[Pacman.X, Pacman.Y].HasBeenEaten ? TileType.Empty : TileType.Pellet);

            Pacman.X = 1;
            Pacman.Y = 1;
        }

        private void UpdateSpritePositions(Direction newDirection, IMaze gameMaze)
        { 
            Pacman.UpdateCurrentDirection(newDirection);
            GameEngine.UpdateSpritePosition(Pacman, gameMaze, GameLogicValidator);
            foreach (var ghostSprite in Ghosts)
            {
                ghostSprite.CurrentDirection = ghostSprite.Behaviour.ChooseDirection();
                GameEngine.UpdateSpritePosition(ghostSprite, gameMaze, GameLogicValidator);
            }
        }
    }
}