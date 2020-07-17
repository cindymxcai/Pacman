using Pacman.Sprites;

namespace Pacman.Factories
{
    public class SpriteFactory : ISpriteFactory
    {
        public ISprite CreateSprite(int x, int y, ISpriteBehaviour behaviour)
        {
            return new Sprite(x, y, behaviour );
        }
    }
}