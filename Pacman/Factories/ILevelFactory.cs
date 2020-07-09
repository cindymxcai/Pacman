using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;

namespace Pacman.Factories
{
    public interface ILevelFactory
    {
        ILevel CreateLevel(IMaze maze);
    }

    public class LevelFactory : ILevelFactory
    {
        private readonly ITileType _ghost;
        private readonly ITileType _pacmanUp;
        private readonly ITileType _pacmanDown;
        private readonly ITileType _pacmanLeft;
        private readonly ITileType _pacmanRight;
        private readonly ITileType _pacmanChomp;
        private readonly ITileType _wallTile;
        private readonly ITileType _emptyTile;
        private readonly ITileType _pelletTile;
        private readonly IDisplay _display;
        private readonly ISpriteFactory _spriteFactory;
        private readonly IGameLogicValidator _gameLogicValidator;
        private readonly IGameEngine _gameEngine;
        private readonly IPlayerInput _playerInput;
        private readonly ISpriteBehaviour _pacmanBehaviour;
        private readonly ISpriteBehaviour _ghostBehaviour;
        
        public LevelFactory(ITileType ghost, ITileType pacmanUp, ITileType pacmanDown, ITileType pacmanLeft, ITileType pacmanRight, ITileType pacmanChomp,ITileType wallTile,ITileType emptyTile, ITileType pelletTile, IDisplay display, ISpriteFactory spriteFactory, IGameLogicValidator gameLogicValidator, IGameEngine gameEngine, IPlayerInput playerInput, ISpriteBehaviour pacmanBehaviour, ISpriteBehaviour ghostBehaviour )
        {
            _ghost = ghost;
            _pacmanUp = pacmanUp;
            _pacmanDown = pacmanDown;
            _pacmanLeft = pacmanLeft;
            _pacmanRight = pacmanRight;
            _pacmanChomp = pacmanChomp;
            _wallTile = wallTile;
            _emptyTile = emptyTile;
            _pelletTile = pelletTile;
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
            return new Level(_ghost, _pacmanUp, _pacmanDown, _pacmanLeft, _pacmanRight, _pacmanChomp,_wallTile, _emptyTile, _pelletTile, maze, _display, _spriteFactory, _gameLogicValidator, _gameEngine, _playerInput, _pacmanBehaviour, _ghostBehaviour);
        }
    }
}