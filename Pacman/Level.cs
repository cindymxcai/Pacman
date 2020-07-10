using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.Sprites;

namespace Pacman
{
    public class Level : ILevel
    {
        private readonly ITileTypeFactory _tileTypeFactory;
        private readonly IMaze _gameMaze;
        private readonly IDisplay _display;
        private readonly IPlayerInput _playerInput;
        public readonly ISprite Pacman;
        public readonly List<ISprite> Ghosts = new List<ISprite>();
        public int LivesLeft { get; set; }
        public bool HasWon { get; private set; }
        public int LevelScore { get; private set; }
        public IGameEngine GameEngine { get; }
        public IGameLogicValidator GameLogicValidator { get; }

        public Level(ITileTypeFactory tileTypeFactory,  IMaze maze,  IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour)
        {
            GameLogicValidator = gameLogicValidator;
            GameEngine = gameEngine;

            _tileTypeFactory = tileTypeFactory;
            _gameMaze = maze;
            _display = display;
            _playerInput = playerInput;
            Pacman = spriteFactory.CreateSprite(1, 1, pacmanBehaviour);
            Ghosts.Add(spriteFactory.CreateSprite(9,9, ghostBehaviour));
            Ghosts.Add(spriteFactory.CreateSprite(9,10, ghostBehaviour));
            HasWon = false;
            LivesLeft = 3;
        }

        public void PlayLevel()
        {
            while (!HasWon)
            {
                var newDirection = _playerInput.TakeInput(Pacman.CurrentDirection);
                var isChomping = false;

                while (!_playerInput.HasNewInput())
                {
                    var remainingPellets =
                        _gameMaze.MazeArray.Cast<Tile>().Count(tile => tile.TileType.Display == _tileTypeFactory.Pellet.Display);
                    
                    LevelScore = Score.GetTotal(_gameMaze.Pellets, remainingPellets);
                    
                    UpdateSpritePositions(newDirection);
                    GameEngine.UpdateMazeTileDisplays(_tileTypeFactory,isChomping, _gameMaze, Pacman, Ghosts);
                    
                    if (GameLogicValidator.HasCollidedWithGhost(Pacman, Ghosts)) 
                        HandleDeath();
                    
                    if (LivesLeft == 0)
                        break;

                    HasWon = GameLogicValidator.HasEatenAllPellets(remainingPellets);
                    
                    Console.Clear();

                    _display.OutputMaze(_gameMaze); 
                    _display.GameStats(LevelScore, LivesLeft);

                    isChomping = !isChomping;
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.2));
                }
                
                if (LivesLeft != 0) continue;
                break;
            }
        }

        public void HandleDeath()
        {
            LivesLeft--;
            _display.LostLife(LivesLeft);
            _gameMaze.UpdateMazeArray(Pacman.X, Pacman.Y,
                _gameMaze.MazeArray[Pacman.X, Pacman.Y].HasBeenEaten ? _tileTypeFactory.Empty: _tileTypeFactory.Pellet);

            Pacman.X = 1;
            Pacman.Y = 1;
        }

        private void UpdateSpritePositions(Direction newDirection)
        { 
            Pacman.UpdateCurrentDirection(newDirection);
            GameEngine.UpdateSpritePosition(_tileTypeFactory.Wall, Pacman, _gameMaze, GameLogicValidator);
            foreach (var ghostSprite in Ghosts)
            {
                ghostSprite.CurrentDirection = ghostSprite.Behaviour.ChooseDirection();
                GameEngine.UpdateSpritePosition(_tileTypeFactory.Wall, ghostSprite, _gameMaze, GameLogicValidator);
            }
        }
    }
}