using Pacman.Enums;

namespace Pacman.Sprites
{
    public interface IGhostBehaviour
    { 
        Direction ChooseDirection();
    }
}