using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Interfaces;

namespace Pacman
{
    public class Maze : IMaze
    {
        public ITile[,] MazeArray { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; } 
        public int Pellets { get; private set; }

        private readonly ITileTypeFactory _tileFactory;
        
        public Maze(IReadOnlyList<string> mazeData, ITileTypeFactory tileTypeFactory)
        {
           CreateMaze(mazeData, tileTypeFactory.Pellet);
           _tileFactory = tileTypeFactory;
        }

        private void CreateMaze(IReadOnlyList<string> mazeData, ITileType pellet)
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
                    if (tileType.Display == pellet.Display)
                    {
                        Pellets++;
                    }
                    y++;
                }
                x++;
            }
        }

        public void UpdateMazeArray(int x, int y, ITileType tileType, ITileType empty)
        {
            MazeArray[x, y].TileType = tileType;
            if (MazeArray[x, y].TileType.Display == empty.Display)
            {
                MazeArray[x, y].HasBeenEaten = true; //TODO remove
            }
        }
        
        public void Render()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    _tileFactory.DisplayTile(MazeArray[i,j]);
                }
                Console.WriteLine();
            }
        }

        public bool HasEatenAllPellets(int pelletsEaten)
        {
            return pelletsEaten == Pellets;
        }
    }
}