﻿using Pacman.Factories;
 using Pacman.Sprites;

 namespace Pacman
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var display = new Display();
            var fileReader = new FileReader();
            var gameLogicValidator = new GameLogicValidator();
            var gameEngine = new GameEngine(display);
            var playerInput = new PlayerInput();
            var mazeFactory = new MazeFactory();
            var spriteFactory = new SpriteFactory();
            var pacmanBehaviour  = new PacmanBehaviour();
            var ghostBehaviour = new RandomGhostBehaviour();
            var gameSettingLoader = new GameSettingLoader(fileReader);
            var levelFactory = new LevelFactory();
            var game = new Game(levelFactory, gameSettingLoader,display, spriteFactory, gameLogicValidator, gameEngine, mazeFactory, fileReader, playerInput, pacmanBehaviour, ghostBehaviour);
            game.PlayGame();
        }
        
    }
}