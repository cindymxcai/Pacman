using System;
using Pacman.Enums;

namespace Pacman
{
    public static class Parser
    {
        public static TileType GetTileType(char inputChar)
        {
            switch (inputChar)
            {
                case '*':
                    return TileType.Wall;
                case '.':
                    return TileType.Pellet;
                default:
                    throw new Exception();
            }
        }
    }
}