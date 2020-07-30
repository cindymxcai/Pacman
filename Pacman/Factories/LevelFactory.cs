using Pacman.Interfaces;

namespace Pacman.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly ITileTypeFactory _tileTypeFactory;
        private readonly IDisplay _display;
        private readonly ISpriteFactory _spriteFactory;
        private readonly IGameLogicValidator _gameLogicValidator;
        private readonly IGameEngine _gameEngine;
        private readonly IPlayerInput _playerInput;
        private readonly ISpriteBehaviour _pacmanBehaviour;
        private readonly ISpriteBehaviour _ghostBehaviour;

        public LevelFactory(ITileTypeFactory tileTypeFactory, IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour )
        {
            _tileTypeFactory = tileTypeFactory;
            _display = display;
            _spriteFactory = spriteFactory;
            _gameLogicValidator = gameLogicValidator;
            _gameEngine = gameEngine;
            _playerInput = playerInput;
            _pacmanBehaviour = pacmanBehaviour;
            _ghostBehaviour = ghostBehaviour;
        }
        public ILevel CreateLevel(IMaze maze)
        {
            return new Level(_tileTypeFactory, maze, _display, _spriteFactory, _gameLogicValidator, _gameEngine, _playerInput, _pacmanBehaviour, _ghostBehaviour);
        }
    }
}