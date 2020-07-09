using System.Collections.Generic;
using System.Linq;
using Pacman.Enums;
using Pacman.Interfaces;
using Pacman.TileTypes;

namespace Pacman
{
    public class Maze : IMaze
    {
        public ITile[,] MazeArray { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; } 
        public int Pellets { get; private set; }

        public Maze(IReadOnlyList<string> mazeData)
        {
           CreateMaze(mazeData);
        }

        private void CreateMaze(IReadOnlyList<string> mazeData)
        {
            Height = mazeData.Count;
            Width = mazeData[0].Length;
            MazeArray = new ITile[Height, Width];
            
            var x = 0;
            foreach (var lineData in mazeData)
            {
                var y = 0;
                foreach (var tileType in lineData.Select(Parser.GetTileType))
                {
                    MazeArray[x, y] = new Tile(tileType);
                    if (tileType.Display == new PelletTile().Display)
                    {
                        Pellets++;
                    }
                    y++;
                }
                x++;
            }
        }

        public void UpdateMazeArray(int x, int y, ITileType tileType)
        {
            MazeArray[x, y].TileType = tileType;
            if (MazeArray[x, y].TileType.Display == new EmptyTile().Display)
            {
                MazeArray[x, y].HasBeenEaten = true;
            }
        }
    }
}