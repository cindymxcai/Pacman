﻿﻿using Pacman.Factories;
 using Pacman.Sprites;
 using Pacman.TileTypes;

 namespace Pacman
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            var pacmanChompTile = new PacmanChompTile();
            var pacmanUpTile = new PacmanUpTile();
            var pacmanDownTile = new PacmanDownTile();
            var pacmanLeftTile = new PacmanLeftTile();
            var pacmanRightTile = new PacmanRightTile();
            var ghostTile = new GhostTile();
            
            var tileTypeFactory = new TileTypeFactory(wallTile, emptyTile, pelletTile, pacmanChompTile, pacmanUpTile, pacmanDownTile, pacmanLeftTile, pacmanRightTile, ghostTile);
            var tileDisplay = new TileFactory();
            var display = new Display(tileDisplay);
            var fileReader = new FileReader();
            var gameLogicValidator = new GameLogicValidator();
            var gameEngine = new GameEngine();
            var playerInput = new PlayerInput();
            var mazeFactory = new MazeFactory(fileReader);
            var spriteFactory = new SpriteFactory();
            var pacmanBehaviour  = new PacmanBehaviour(pacmanUpTile, pacmanDownTile, pacmanLeftTile, pacmanRightTile);
            var ghostBehaviour = new RandomGhostBehaviour();
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var levelFactory = new LevelFactory(tileTypeFactory, display, spriteFactory, gameLogicValidator, gameEngine, playerInput, pacmanBehaviour, ghostBehaviour);
            var game = new Game(levelFactory, gameSettingLoader,display, mazeFactory, playerInput);
            game.PlayGame();
        }
        
    }
}