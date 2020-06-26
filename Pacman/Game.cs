using System;
namespace Pacman
{
    public static class Game
    {
        private static bool IsPlaying { get; set; } = true;
        private static int CurrentLevelNumber { get; set; }
        private static readonly IFileReader FileReader = new FileReader();

        public static void PlayGame()
        {
            CurrentLevelNumber = 1;
            Display.Welcome();
            while (IsPlaying)
            {
                try
                {
                    var level = new Level(CurrentLevelNumber, FileReader);
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