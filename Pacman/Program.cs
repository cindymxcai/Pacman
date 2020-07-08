﻿using Pacman.Factories;
 using Pacman.Sprites;

 namespace Pacman
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var tileDisplay = new TileFactory();
            var display = new Display(tileDisplay);
            var fileReader = new FileReader();
            var gameLogicValidator = new GameLogicValidator();
            var gameEngine = new GameEngine();
            var playerInput = new PlayerInput();
            var mazeFactory = new MazeFactory(fileReader);
            var spriteFactory = new SpriteFactory();
            var pacmanBehaviour  = new PacmanBehaviour();
            var ghostBehaviour = new RandomGhostBehaviour();
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var levelFactory = new LevelFactory(display, spriteFactory, gameLogicValidator, gameEngine, playerInput, pacmanBehaviour, ghostBehaviour);
            var game = new Game(levelFactory, gameSettingLoader,display, mazeFactory, playerInput);
            game.PlayGame();
        }
        
    }
}