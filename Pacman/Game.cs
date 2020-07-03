using System;
using System.IO;
using Newtonsoft.Json;
using Pacman.Sprites;

namespace Pacman
{
    public class Game
    {
        private readonly IGameSettingLoader _gameSettingLoader;
        private readonly IDisplay _display;
        private readonly ISpriteFactory _spriteFactory;
        private readonly IGameLogicValidator _gameLogicValidator;
        private readonly IGameEngine _gameEngine;
        private readonly IMazeFactory _mazeFactory;
        private readonly IFileReader _fileReader;
        private readonly IPlayerInput _playerInput;
        private readonly ISpriteBehaviour _pacmanBehaviour;
        private readonly ISpriteBehaviour _ghostBehaviour;
        private bool IsPlaying { get; set; } = true;
        private int CurrentLevelNumber { get; set; }

        public Game(IGameSettingLoader gameSettingLoader, IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator,
            IGameEngine gameEngine, IMazeFactory mazeFactory, IFileReader fileReader, IPlayerInput playerInput,
            ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour)
        {
            _gameSettingLoader = gameSettingLoader;
            _display = display;
            _spriteFactory = spriteFactory;
            _gameLogicValidator = gameLogicValidator;
            _gameEngine = gameEngine;
            _mazeFactory = mazeFactory;
            _fileReader = fileReader;
            _playerInput = playerInput;
            _pacmanBehaviour = pacmanBehaviour;
            _ghostBehaviour = ghostBehaviour;
        }

        public void PlayGame()
        {
            CurrentLevelNumber = 1;
            _display.Welcome();
            var gameSettings = _gameSettingLoader.GetLevelData(); 
            
            while (IsPlaying)
            {
                var mazeData = _fileReader.ReadFile(gameSettings.LevelSettings[CurrentLevelNumber - 1]);
                var maze = _mazeFactory.CreateMaze(mazeData);
                
                var level = new Level(_display, _spriteFactory, _gameLogicValidator,  _gameEngine, _playerInput, _pacmanBehaviour, _ghostBehaviour );
                
                level.PlayLevel(maze);
                
                if (level.HasWon)
                {
                    _display.CongratulationsNewLevel(CurrentLevelNumber);
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    CurrentLevelNumber++;
                }
                
                else
                {
                    HandleLostLevel();
                }

                if (CurrentLevelNumber >= gameSettings.MaxLevels)
                {
                    IsPlaying = false;
                }
            }

            Display.WonGame();
        }

        
        private void HandleLostLevel()
        {
            Console.WriteLine("\nPress enter to replay, or Q to quit");
            if (_playerInput.isPressedQuit())
            {
                _display.GameEnd(CurrentLevelNumber);

            }
            else
            {
                CurrentLevelNumber = 1;
            }
        }
    }
}