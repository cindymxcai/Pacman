namespace Pacman.Interfaces
{
    public interface ILevel
    {
        bool HasWon { get; }
        void PlayLevel();
    }
}