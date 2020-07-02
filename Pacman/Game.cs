using System;
using System.IO;
using Newtonsoft.Json;

namespace Pacman
{
    public  class Game
    {
        private readonly IMazeFactory _mazeFactory;
        private readonly Level _level;
        private readonly IFileReader _fileReader;
        private readonly IPlayerInput _playerInput;
        private  bool IsPlaying { get; set; } = true;
        private int CurrentLevelNumber { get; set; }
        
        public Game(IMazeFactory mazeFactory,Level level, IFileReader fileReader, IPlayerInput playerInput)
        {
            _mazeFactory = mazeFactory;
            _level = level;
            _fileReader = fileReader;
            _playerInput = playerInput;
        }
        public void PlayGame()
        {
            CurrentLevelNumber = 1;
            Display.Welcome();
            var levelData = GetLevelData();

            while (IsPlaying)
            {
                try
                {
                    var mazeData = _fileReader.ReadFile(levelData.levels[CurrentLevelNumber - 1]);
                    var maze = _mazeFactory.CreateMaze(mazeData);
                    _level.PlayLevel(maze);
                    
                    if (_level.HasWon)
                    {
                        Display.CongratulationsNewLevel(CurrentLevelNumber);
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                        CurrentLevelNumber++;
                    }
                    else
                    {
                        HandleLostLevel();
                    }
                }
                catch
                {
                    IsPlaying = false;
                }
            }
            
            Display.WonGame();
        } 

        private static LevelData GetLevelData()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            return JsonConvert.DeserializeObject<LevelData>(json);
        }

        private void HandleLostLevel()
        {
            Console.WriteLine("\nPress enter to replay, or Q to quit");
            if (_playerInput.IsStillPlaying())
            {
                CurrentLevelNumber = 1;
            }
            else
            {
                Display.GameEnd(CurrentLevelNumber);
            }
        }
    }
}