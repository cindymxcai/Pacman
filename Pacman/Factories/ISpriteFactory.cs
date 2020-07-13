using Pacman.Sprites;

namespace Pacman.Factories
{
    public interface ISpriteFactory
    {
        ISprite CreateSprite(int x, int y, ISpriteBehaviour behaviour, ISpriteDisplay spriteDisplay);
    }

    public class SpriteFactory : ISpriteFactory
    {
        public ISprite CreateSprite(int x, int y, ISpriteBehaviour behaviour, ISpriteDisplay spriteDisplay)
        {
            return new Sprite(x, y, behaviour, spriteDisplay );
        }
    }
}