using System.Collections.Generic;
using Pacman.Sprites;

namespace Pacman
{
    public interface IGameEngine
    {
        void UpdateMazeTileDisplays(bool isChomping, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts);

        void UpdateSpritePosition(ISprite sprite, IMaze gameMaze, IGameLogicValidator gameLogicValidator);
        (int, int) GetNewPosition(ISprite sprite, IMaze gameMaze);
    }
}