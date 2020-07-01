using System;
using System.IO;
using Newtonsoft.Json;

namespace Pacman
{
    public static class Game
    {
        private static bool IsPlaying { get; set; } = true;
        private static int CurrentLevelNumber { get; set; }
        private static readonly IFileReader FileReader = new FileReader();
        private static readonly IGameLogicValidator GameLogicValidator = new GameLogicValidator();
        private static readonly IGameEngine GameEngine = new GameEngine();
        private static readonly IPlayerInput PlayerInput = new PlayerInput();

        public static void PlayGame()
        {
            CurrentLevelNumber = 1;
            Display.Welcome();
            var levelData = GetLevelData();

            while (IsPlaying)
            {
                try
                {
                    var mazeData = FileReader.ReadFile(levelData.levels[CurrentLevelNumber - 1]);
                    var maze = new Maze(mazeData);
                    var level = new Level(maze, GameLogicValidator,GameEngine, PlayerInput);
                   
                    level.PlayLevel();
                    
                    if (level.HasWon)
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

        private static void HandleLostLevel()
        {
            Console.WriteLine("\nPress enter to replay, or Q to quit");
            var input = Console.ReadKey().Key;
            if (input == ConsoleKey.Q)
            {
                Display.GameEnd(CurrentLevelNumber);
            }
            else
            {
                CurrentLevelNumber = 1;
            }
        }
    }
}