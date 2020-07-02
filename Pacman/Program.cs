﻿using Pacman.Sprites;

 namespace Pacman
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var gameLogicValidator = new GameLogicValidator();
            var gameEngine = new GameEngine();
            var playerInput = new PlayerInput();
            var mazeFactory = new MazeFactory();
            var spriteFactory = new SpriteFactory();
            var pacmanBehaviour  = new PacmanBehaviour();
            var ghostBehaviour = new RandomGhostBehaviour();
            var level = new Level (spriteFactory, gameLogicValidator, gameEngine, playerInput, pacmanBehaviour, ghostBehaviour);
            var game = new Game(mazeFactory, level, fileReader, playerInput);
            game.PlayGame();
        }
        
    }
}