using System;
using Pacman.Enums;
using Pacman.Sprites;

namespace Pacman
{
    public static class Display
    {
        public static void MazeOutput(IMaze maze)
        {
            for (var i = 0; i < maze.Height; i++)
            {
                for (var j = 0; j < maze.Width; j++)
                {
                    switch (maze.MazeArray[i, j].TileType)
                    {
                        case TileType.Wall:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(maze.MazeArray[i, j].Display);
                            Console.ResetColor();
                            break;
                        case TileType.Pellet:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(maze.MazeArray[i, j].Display);
                            Console.ResetColor();
                            break;
                        case TileType.Ghost:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(maze.MazeArray[i, j].Display);
                            Console.ResetColor();
                            break;
                        default:
                            Console.Write(maze.MazeArray[i, j].Display);
                            break;
                    }
                }

                Console.WriteLine();
            }
        }

        public static void GameStats(int score, int livesLeft)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"LevelScore: {score}                                                Lives Left: {livesLeft}");
            Console.ResetColor();
        }

        public static void LostLife(int livesLeft)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" .-. ");
            Console.WriteLine(@"| OO|  ╭⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╮");
            Console.WriteLine(@"|   | <  Lives left: " + livesLeft + " |");
            Console.Write(@"'^^^'  ╰⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼⎼╯");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ResetColor();
        }

        public static void UpdatePacmanDisplay(int counter, IMaze gameMaze, ISprite pacman, Direction direction)
        {
            if (counter % 2 == 0)
            {
                switch (direction)
                {
                    case Direction.Up:
                        gameMaze.MazeArray[pacman.X, pacman.Y].SetTile(TileType.PacmanUp);
                        break;
                    case Direction.Down:
                        gameMaze.MazeArray[pacman.X, pacman.Y].SetTile(TileType.PacmanDown);
                        break;
                    case Direction.Left:
                        gameMaze.MazeArray[pacman.X, pacman.Y].SetTile(TileType.PacmanLeft);
                        break;
                    case Direction.Right:
                        gameMaze.MazeArray[pacman.X, pacman.Y].SetTile(TileType.PacmanRight);
                        break;
                    default:
                        gameMaze.MazeArray[pacman.X, pacman.Y] = gameMaze.MazeArray[pacman.X, pacman.Y];
                        break;
                }
            }
            else
            {
                gameMaze.MazeArray[pacman.X, pacman.Y].SetTile(TileType.PacmanChomp);
            }
        }

        public static void Welcome()
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

        public static void CongratulationsNewLevel(int levelNumber)
        {
            Console.Clear();
            Console.WriteLine(@" ___  ___  _ _  ___   ___  ___  ___  _ _  _    ___  ___  _  ___  _ _  ___");
            Console.WriteLine(@"|  _>| . || \ |/  _> | . \| . ||_ _|| | || |  | . ||_ _|| || . || \ |/ __>");
            Console.WriteLine(@"| <__| | ||   || <_/\|   /|   | | | | ' || |_ |   | | | | || | ||   |\__ \");
            Console.WriteLine(@"`___/`___'|_\_|`____/|_\_\|_|_| |_| `___'|___||_|_| |_| |_|`___'|_\_|<___/");
            Console.WriteLine($"Passed {levelNumber}");
        }

        public static void GameEnd(int levelNumber)
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