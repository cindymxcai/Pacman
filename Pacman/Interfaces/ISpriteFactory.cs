namespace Pacman.Interfaces
{
    public interface ISpriteFactory
    {
        ISprite CreateSprite(int x, int y, ISpriteBehaviour behaviour);
    }
}