using System;
using System.IO;
using Newtonsoft.Json;
using Pacman;
using Pacman.Factories;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;
using Xunit;

namespace PacmanTests
{
    public class GameTest
    {
        
        private ITileTypeFactory SetUpLevel()
        {
            var wallTile = new WallTile();
            var emptyTile = new EmptyTile();
            var pelletTile = new PelletTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile);
        }
        
        private Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            return new Maze(mazeData, SetUpLevel());
        }
        
        [Fact]
        public void ScoreShouldInitiallyBe0()
        {
            var maze = MazeSetUp();
            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            Assert.Equal(0, level.LevelScore);
        }
        
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var maze = MazeSetUp();
            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile()));
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var maze = MazeSetUp();
            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(new GameLogicValidator()), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile()), new RandomGhostBehaviour(new GhostTile())){LivesLeft = 3};
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}