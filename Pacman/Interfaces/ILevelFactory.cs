using Pacman.Interfaces;

namespace Pacman.Factories
{
    public interface ILevelFactory
    {
        ILevel CreateLevel(IMaze maze);
    }
}