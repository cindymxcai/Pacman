using Pacman.Enums;
using Pacman.Factories;
using Pacman.Interfaces;

namespace Pacman.Sprites
{
    public interface ISpriteDisplay
    {
        public void UpdatePacmanDisplay(ITileTypeFactory tileTypeFactory, bool isChomping, IMaze gameMaze,
            ISprite pacman, Direction pacmanCurrentDirection);
    }
}