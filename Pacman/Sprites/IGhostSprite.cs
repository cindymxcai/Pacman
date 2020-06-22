namespace Pacman.Sprites
{
    public interface IGhostSprite : ISprite
    {
        IGhostBehaviour Behaviour { get; }
    }
}