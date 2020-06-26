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
        private static readonly IGameLogicValidator gameLogicValidator = new GameLogicValidator();
        private static readonly IGameEngine gameEngine = new GameEngine();
        private static readonly IPlayerInput playerInput = new PlayerInput();


        public static void PlayGame()
        {
            CurrentLevelNumber = 1;
            Display.Welcome();
            while (IsPlaying)
            {
                
                var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
                var json = File.ReadAllText(jsonFileName);
                var levels = JsonConvert.DeserializeObject<LevelObject>(json);
                
                try
                {
                    var level = new Level(FileReader, levels, CurrentLevelNumber, gameLogicValidator,gameEngine, playerInput);
                    if (level.HasWonLevel())
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

        private static void HandleLostLevel()
        {
            Console.WriteLine("\nPress any key to replay, or Q to quit");
            var input = Console.ReadKey().Key;
            if (input == ConsoleKey.Q)
            {
                IsPlaying = false;
                Display.GameEnd(CurrentLevelNumber);
            }
            else
            {
                CurrentLevelNumber = 1;
            }
        }
    }
}