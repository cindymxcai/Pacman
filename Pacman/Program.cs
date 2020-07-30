﻿using Pacman.Factories;
 using Pacman.Sprites;
 using Pacman.TileTypes;

 namespace Pacman
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var playerInput = new PlayerInput();
            
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            var tileTypeFactory = new TileTypeFactory(wallTile, emptyTile, pelletTile);
            
            var ghostTile = new GhostTile();
            var ghostBehaviour = new RandomGhostBehaviour(ghostTile);
            
            var pacmanTile = new PacmanTile();
            var pacmanBehaviour  = new PacmanBehaviour(pacmanTile);
            
            var fileReader = new FileReader();
            var mazeFactory = new MazeFactory(fileReader, tileTypeFactory);
            var gameSettingLoader = new GameSettingLoader(fileReader);
            
            var gameLogicValidator = new GameLogicValidator();
            var gameEngine = new GameEngine(gameLogicValidator);
            
            var display = new Display();
            var spriteFactory = new SpriteFactory();
            var levelFactory = new LevelFactory(tileTypeFactory, display, spriteFactory, gameLogicValidator, gameEngine, playerInput, pacmanBehaviour, ghostBehaviour);
            
            var game = new Game(levelFactory, gameSettingLoader,display, mazeFactory, playerInput);
            game.PlayGame();
        }
        
    }
}