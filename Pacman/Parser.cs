using System;
using Pacman.TileTypes;

namespace Pacman
{
    public static class Parser
    {
        public static ITileType GetTileType(char inputChar)
        {
            switch (inputChar)
            {
                case '*':
                    return new WallTile();
                case '.':
                    return new PelletTile();
                default:
                    throw new Exception();
            }
        }
    }
}