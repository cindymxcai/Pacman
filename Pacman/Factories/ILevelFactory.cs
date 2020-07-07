using Pacman.Sprites;

namespace Pacman.Factories
{
    public interface ILevelFactory
    {
        ILevel CreateLevel(IMaze maze, IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour);
    }

    public class LevelFactory : ILevelFactory
    {
      

        public ILevel CreateLevel(IMaze maze, IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator,
            IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour,
            ISpriteBehaviour ghostBehaviour)
        {
            return new Level(maze, display, spriteFactory, gameLogicValidator, gameEngine, playerInput, pacmanBehaviour, ghostBehaviour);
        }
    }
}