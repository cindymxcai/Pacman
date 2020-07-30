using System.Collections.Generic;

namespace Pacman.Interfaces
{
    public interface IGameEngine
    {
        void UpdateMazeTileDisplays(ITileTypeFactory tileTypeFactory, IMaze gameMaze, ISprite pacman,
            IEnumerable<ISprite> ghosts);

        void UpdateSpritePosition(ITileTypeFactory tileTypeFactory, ISprite sprite, IMaze gameMaze);
        (int, int) GetNewPosition(ISprite sprite, IMaze gameMaze);
    }
}