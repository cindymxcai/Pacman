using System;
using Pacman.Factories;
using Pacman.Interfaces;

namespace Pacman
{
    public class Display : IDisplay
    {
        private readonly ITileTypeFactory _tileFactory;
        public Display(ITileTypeFactory tileFactory)
        {
            _tileFactory = tileFactory;
        }
        
        public void OutputMaze(IMaze maze)
        {
            for (var i = 0; i < maze.Height; i++)
            {
                for (var j = 0; j < maze.Width; j++)
                {
                    _tileFactory.DisplayTile(maze.MazeArray[i,j]);
                }
                Console.WriteLine();
            }
        }

        public void GameStats(int score, int livesLeft)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"LevelScore: {score}                                                Lives Left: {livesLeft}");
            Console.ResetColor();
        }

        public void LostLife(int livesLeft)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" .-. ");
            Console.WriteLine(@"| OO|  ╭⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╮");
            Console.WriteLine(@"|   | <  Lives left: " + livesLeft + " |");
            Console.WriteLine(@"'^^^'  ╰⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╯");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ResetColor();
        }
        

        public void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@" ______    ______     ______     __    __     ______     __   __    ");
            Console.WriteLine(@"/\  == \  /\  __ \   /\  ___\   /\ `-./  \   /\  __ \   /\ `-.\ \   ");
            Console.WriteLine(@"\ \  _-/  \ \  __ \  \ \ \____  \ \ \-./\ \  \ \  __ \  \ \ \-.  \ ");
            Console.WriteLine(@" \  \_\    \ \_\ \_\  \ \_____\  \ \_\ \ \_\  \ \_\ \_\  \ \_\\ \_\ ");
            Console.WriteLine(@"  \/_/      \/_/\/_/   \/_____/   \/_/  \/_/   \/_/\/_/   \/_/ \/_/");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPress any key to play!");
            Console.ResetColor();
        }

        public void CongratulationsNewLevel(int levelNumber)
        {
            Console.Clear();
            Console.WriteLine(@" ___  ___  _ _  ___   ___  ___  ___  _ _  _    ___  ___  _  ___  _ _  ___");
            Console.WriteLine(@"|  _>| . || \ |/  _> | . \| . ||_ _|| | || |  | . ||_ _|| || . || \ |/ __>");
            Console.WriteLine(@"| <__| | ||   || <_/\|   /|   | | | | ' || |_ |   | | | | || | ||   |\__ \");
            Console.WriteLine(@"`___/`___'|_\_|`____/|_\_\|_|_| |_| `___'|___||_|_| |_| |_|`___'|_\_|<___/");
            Console.WriteLine($"Passed {levelNumber}");
        }

        public void GameEnd(int levelNumber)
        {
            Console.Clear();
            Console.WriteLine(@" ___  _              _         ___                 _            _            _ ");
            Console.WriteLine(@"|_ _|| |_  ___ ._ _ | |__ ___ | | '___  _ _   ___ | | ___  _ _ <_>._ _  ___ | |");
            Console.WriteLine(@" | | | . |<_> || ' || / /<_-< | |-/ . \| '_> | . \| |<_> || | || || ' |/ . ||_/");
            Console.WriteLine(@" |_| |_|_|<___||_|_||_\_\/__/ |_| \___/|_|   |  _/|_|<___|`_. ||_||_|_|\_. |<_>");
            Console.WriteLine(@"                                             |_|          <___'        <___'   ");
            Console.WriteLine($"You made it to level {levelNumber}");
        }

        public static void WonGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@" ___  ___  _ _  ___   ___  ___  ___  _ _  _    ___  ___  _  ___  _ _  ___");
            Console.WriteLine(@"|  _>| . || \ |/  _> | . \| . ||_ _|| | || |  | . ||_ _|| || . || \ |/ __>");
            Console.WriteLine(@"| <__| | ||   || <_/\|   /|   | | | | ' || |_ |   | | | | || | ||   |\__ \");
            Console.WriteLine(@"`___/`___'|_\_|`____/|_\_\|_|_| |_| `___'|___||_|_| |_| |_|`___'|_\_|<___/");
            Console.WriteLine("You finished the game!!!");
            Console.ResetColor();
        }
    }
}