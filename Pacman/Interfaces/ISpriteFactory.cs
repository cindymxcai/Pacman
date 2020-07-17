using Pacman.Sprites;

namespace Pacman.Factories
{
    public interface ISpriteFactory
    {
        ISprite CreateSprite(int x, int y, ISpriteBehaviour behaviour);
    }
}