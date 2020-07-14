using System;
using System.IO;
using Newtonsoft.Json;
using Pacman;
using Pacman.Factories;
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
            var pacmanChompTile = new PacmanChompTile();
            var pacmanUpTile = new PacmanUpTile();
            var pacmanDownTile = new PacmanDownTile();
            var pacmanLeftTile = new PacmanLeftTile();
            var pacmanRightTile = new PacmanRightTile();
            var ghostTile = new GhostTile();
            return new TileTypeFactory(wallTile, emptyTile, pelletTile, pacmanChompTile, pacmanUpTile, pacmanDownTile, pacmanLeftTile, pacmanRightTile, ghostTile);
        }
        
        private Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<GameSettings>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.LevelSettings[1]);
            return new Maze(mazeData);
        }
        
        [Fact]
        public void ScoreShouldInitiallyBe0()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());
            Assert.Equal(0, level.LevelScore);
        }
        
        
        [Fact]
        public void UpdatesMazeWithCorrectTile()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( new PelletTile(),  level.Pacman, maze, level.GameLogicValidator);
            level.GameEngine.UpdateMazeTileDisplays(tileTypeFactory, maze, level.Pacman, level.Ghosts);

            Assert.Equal(new EmptyTile().Display, maze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].TileType.Display);
            var tile = new Tile(new PacmanRightTile());
            Assert.Equal(tile.TileType.Display, maze.MazeArray[level.Pacman.X, level.Pacman.Y].TileType.Display);
        }
        
        [Fact]
        public void GhostCollisionIsTrueIfGhostAndPacmanAreOnSameTileOrPassEachOther()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var tileTypeFactory = SetUpLevel();
            var level = new Level( tileTypeFactory, maze, new Display(tileFactory), new SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(),
                new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour()) {Pacman = {X = 3, Y = 3}};
            level.Ghosts[0].X = 3;
            level.Ghosts[0].Y = 3;
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var tileTypeFactory = SetUpLevel(); 
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());
            Assert.True(level.GameLogicValidator.HasEatenAllPellets(0));
            Assert.False(level.GameLogicValidator.HasEatenAllPellets(2));
        }
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour());
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();

            var tileTypeFactory = SetUpLevel();
            var level = new Level(tileTypeFactory, maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile()), new RandomGhostBehaviour()){LivesLeft = 3};
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}