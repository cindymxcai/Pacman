using System;
using System.IO;
using Newtonsoft.Json;
using Pacman.Factories;
using Pacman.Sprites;

namespace Pacman
{
    public class Game
    {
        private readonly ILevelFactory _levelFactory;
        private readonly IGameSettingLoader _gameSettingLoader;
        private readonly IDisplay _display;
        private readonly IMazeFactory _mazeFactory;
        private readonly IPlayerInput _playerInput;
        private bool IsPlaying { get; set; } = true;
        private int CurrentLevelNumber { get; set; }

        public Game(ILevelFactory levelFactory, IGameSettingLoader gameSettingLoader, IDisplay display, IMazeFactory mazeFactory, IPlayerInput playerInput)      
        {
            _levelFactory = levelFactory;
            _gameSettingLoader = gameSettingLoader;
            _display = display;
            _mazeFactory = mazeFactory;
            _playerInput = playerInput;
        }

        public void PlayGame()
        {
            CurrentLevelNumber = 1;
            _display.Welcome();
            var gameSettings = _gameSettingLoader.GetLevelData(); 
            
            while (IsPlaying)
            {
                var maze = _mazeFactory.CreateMaze( gameSettings, CurrentLevelNumber);
                var level = _levelFactory.CreateLevel(maze);
                
                level.PlayLevel();
                
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
            
            if (_playerInput.HasPressedQuit())
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