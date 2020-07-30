using System;
using Pacman.Interfaces;

namespace Pacman
{
    public class Game
    {
        /// <summary>
        /// The Game object controls the highest level of the Pacman game, ie controlling the flow before and after
        /// a level is played, as well as readying up Level dependencies and calling each level to be played
        /// </summary>
        private readonly ILevelFactory _levelFactory;

        private readonly IGameSettingLoader _gameSettingLoader;
        private readonly IDisplay _display;
        private readonly IMazeFactory _mazeFactory;
        private readonly IPlayerInput _playerInput;
        private bool IsPlaying { get; set; } = true;
        private int CurrentLevelNumber { get; set; }

        public Game(ILevelFactory levelFactory, IGameSettingLoader gameSettingLoader, IDisplay display,
            IMazeFactory mazeFactory, IPlayerInput playerInput)
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
            var mazeData = _gameSettingLoader.GetMazeData();
            while (IsPlaying)
            {
                var maze = _mazeFactory.CreateMaze(mazeData.LevelSettings[CurrentLevelNumber]);
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

                if (CurrentLevelNumber >= mazeData.MaxLevels)
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