using System;
using System.IO;
using Moq;
using Newtonsoft.Json;
using Pacman;
using Pacman.Enums;
using Pacman.Factories;
using Pacman.Sprites;
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
            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.Equal(0, level.LevelScore);
        }
        
        
        [Theory]
        [InlineData( false, TileType.PacmanRight)]
        [InlineData( true, TileType.PacmanChomp)]
        public void UpdatesMazeWithCorrectTile(bool isChomping, TileType tileType)
        {
            var maze = MazeSetUp();

            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            level.GameEngine.GetNewPosition(level.Pacman, maze);
            level.GameEngine.UpdateSpritePosition( level.Pacman, maze, level.GameLogicValidator);
            level.GameEngine.UpdateMazeTileDisplays(isChomping, maze, level.Pacman, level.Ghosts);

            Assert.Equal(new Tile(TileType.Empty).Display, maze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].Display);
            var tile = new Tile(tileType);
            Assert.Equal(tile.Display, maze.MazeArray[level.Pacman.X, level.Pacman.Y].Display);
        }
        
        [Fact]
        public void GhostCollisionIsTrueIfGhostAndPacmanAreOnSameTileOrPassEachOther()
        {
            var maze = MazeSetUp();

            var level = new Level(maze, new Display(), new SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(),
                new PacmanBehaviour(), new RandomGhostBehaviour()) {Pacman = {X = 3, Y = 3}};
            level.Ghosts[0].X = 3;
            level.Ghosts[0].Y = 3;
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var maze = MazeSetUp();

            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.True(level.GameLogicValidator.HasEatenAllPellets(0));
            Assert.False(level.GameLogicValidator.HasEatenAllPellets(2));
        }
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var maze = MazeSetUp();

            var level = new Level(maze, new Display(), new  SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour());
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var maze = MazeSetUp();
            var level = new Level(maze, new Display(), new SpriteFactory(), new GameLogicValidator(), new GameEngine(), new PlayerInput(),
                new PacmanBehaviour(), new RandomGhostBehaviour()) {LivesLeft = 3};
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}