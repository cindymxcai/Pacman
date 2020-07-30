namespace Pacman.Interfaces
{
    public interface ILevelFactory
    {
        ILevel CreateLevel(IMaze maze);
    }
}