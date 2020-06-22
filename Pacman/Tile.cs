using System;
using Pacman.Enums;

namespace Pacman
{
    public class Tile
    {
        public TileType TileType { get; set; }
        public string Display { get; private set; }
        public bool HasBeenEaten { get; private set; }
        
        private const string TilePellet = " \u2022 ";
        private const string TileWall = "\u2588\u2588\u2588";
        private const string TileEmpty = "   ";
        private const string Up = " \u15E2 ";
        private const string Down = " \u15E3 ";
        private const string Left = " \u15E4 ";
        private const string Right = " \u15E7 ";
        private const string PacmanChomp = " \u25EF ";
        private const string Ghost = " \u1571 ";
       
        public Tile(TileType tileType)
        {
            SetTile(tileType);
        }

        public void SetTile(TileType tileType)
        {
            TileType = tileType;
            switch (tileType)
            {
                case TileType.Empty:
                    Display = TileEmpty;
                    HasBeenEaten = true;
                    break;
                case TileType.Wall:
                    Display = TileWall;
                    break;
                case TileType.Pellet:
                    Display = TilePellet;
                    break;
                case TileType.PacmanUp:
                    Display = Up;
                    break;
                case TileType.PacmanDown:
                    Display = Down;
                    break;
                case TileType.PacmanLeft:
                    Display = Left;
                    break;
                case TileType.PacmanRight:
                    Display = Right;
                    break;
                case TileType.PacmanChomp:
                    Display = PacmanChomp;
                    break;
                case TileType.Ghost:
                    Display = Ghost;
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}