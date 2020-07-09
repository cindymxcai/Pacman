using System.Collections.Generic;
using Pacman.Factories;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman.Interfaces
{
    public interface IGameEngine
    {
        void UpdateMazeTileDisplays(ITileTypeFactory tileTypeFactory,bool isChomping, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts);

        void UpdateSpritePosition(ITileType tileType, ISprite sprite, IMaze gameMaze, IGameLogicValidator gameLogicValidator);
        (int, int) GetNewPosition(ISprite sprite, IMaze gameMaze);
    }
}