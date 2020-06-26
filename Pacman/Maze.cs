using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Pacman.Enums;

namespace Pacman
{
    public class Maze : IMaze
    {
        public Tile[,] MazeArray { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; } 
        public int Pellets { get; private set; }

        public Maze(int level, IFileReader fileReader)
        {
           var mazeData=  GetMazeData(level, fileReader);
           CreateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> mazeData)
        {
            Height = mazeData.Count;
            Width = mazeData[0].Length;
            MazeArray = new Tile[Height, Width];
            
            var x = 0;
            foreach (var lineData in mazeData)
            {
                var y = 0;
                foreach (var tileType in lineData.Select(Parser.GetTileType))
                {
                    MazeArray[x, y] = new Tile(tileType);
                    if (tileType == TileType.Pellet)
                    {
                        Pellets++;
                    }
                    y++;
                }
                x++;
            }
        }

        private static string[] GetMazeData(int level, IFileReader fileReader)
        {
            string[] mazeData = null;
            string jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            LevelObject levels = JsonConvert.DeserializeObject<LevelObject>(json);

            return fileReader.ReadFile(levels.levels[level-1]);
        }

        public void UpdateMazeArray(int x, int y, TileType tileType)
        {
            MazeArray[x, y].SetTile(tileType);
        }
    }
}