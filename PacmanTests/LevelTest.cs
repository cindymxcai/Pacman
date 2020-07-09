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

            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.Equal(0, level.LevelScore);
        }
        
        
        [Fact]
        public void UpdatesMazeWithCorrectTile()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();

            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( new PelletTile(),  level.Pacman, maze, level.GameLogicValidator);
            level.GameEngine.UpdateMazeTileDisplays(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new EmptyTile(), new PelletTile(), true, maze, level.Pacman, level.Ghosts);

            Assert.Equal(new EmptyTile().Display, maze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].TileType.Display);
            var tile = new Tile(new PacmanChompTile());
            Assert.Equal(tile.TileType.Display, maze.MazeArray[level.Pacman.X, level.Pacman.Y].TileType.Display);
        }
        
        [Fact]
        public void GhostCollisionIsTrueIfGhostAndPacmanAreOnSameTileOrPassEachOther()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(),  new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(),
                new PacmanBehaviour(), new RandomGhostBehaviour()) {Pacman = {X = 3, Y = 3}};
            level.Ghosts[0].X = 3;
            level.Ghosts[0].Y = 3;
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();
            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.True(level.GameLogicValidator.HasEatenAllPellets(0));
            Assert.False(level.GameLogicValidator.HasEatenAllPellets(2));
        }
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();

            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var maze = MazeSetUp();
            var tileFactory = new TileFactory();

            var level = new Level(new GhostTile(), new PacmanUpTile(), new PacmanDownTile(), new PacmanLeftTile(), new PacmanRightTile(), new PacmanChompTile(), new WallTile(), new EmptyTile(), new PelletTile(), maze, new Display(tileFactory), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour()){LivesLeft = 3};
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}