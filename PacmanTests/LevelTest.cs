using System;
using System.IO;
using Moq;
using Newtonsoft.Json;
using Pacman;
using Pacman.Enums;
using Pacman.Sprites;
using Xunit;

namespace PacmanTests
{
    public class GameTest
    {
        private Maze MazeSetUp()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<LevelData>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.levels[1]);
            return new Maze(mazeData);
        }
        
        [Fact]
        public void ScoreShouldInitiallyBe0()
        {
           var maze = MazeSetUp();
            var level = new Level(maze, new GameLogicValidator(), new GameEngine(), new PlayerInput());
            Assert.Equal(0, level.Score);
        }
        
        [Fact]
        public void LivesLeftShouldStartAt3()
        {
            var maze = MazeSetUp();
            var level = new Level( maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput());
            Assert.Equal(3, level.LivesLeft);
        }
        
        [Theory]
        [InlineData( 2, TileType.PacmanRight)]
        [InlineData( 1, TileType.PacmanChomp)]
        public void UpdatesMazeWithCorrectTile(int counter, TileType tileType)
        {
            var maze = MazeSetUp();

            var level = new Level(maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput());
            level.GameEngine.GetNewPosition(level.Pacman, level.GameMaze);
            level.GameEngine.UpdateSpritePosition( level.Pacman, level.GameMaze, level.GameLogicValidator);
            level.GameEngine.UpdateMazeTileDisplays(counter, level.GameMaze, level.Pacman, level.Ghosts);

            Assert.Equal(new Tile(TileType.Empty).Display, level.GameMaze.MazeArray[level.Pacman.PrevX, level.Pacman.PrevY].Display);
            var tile = new Tile(tileType);
            Assert.Equal(tile.Display, level.GameMaze.MazeArray[level.Pacman.X, level.Pacman.Y].Display);
        }
        
        [Fact]
        public void GhostCollisionIsTrueIfGhostAndPacmanAreOnSameTileOrPassEachOther()
        {
            var maze = MazeSetUp();

            var level = new Level(maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput()) {Pacman = new Sprite(0, 0, new PacmanBehaviour())};
            var mockGhost = new Mock<ISpriteBehaviour>();
            mockGhost.Setup(ghostBehaviour => ghostBehaviour.ChooseDirection()).Returns(Direction.Left);
            level.Ghosts[1] = new Sprite(0,0, mockGhost.Object);
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var maze = MazeSetUp();

            var level = new Level(maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput());
            Assert.True(level.GameLogicValidator.HasEatenAllPellets(0));
            Assert.False(level.GameLogicValidator.HasEatenAllPellets(2));
        }
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var maze = MazeSetUp();
            var level = new Level(maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput()) {LivesLeft = 0};
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HasWonLevelIfLivesLeftIsNot0AndHasWon()
        {
            var maze = MazeSetUp();

            var level = new Level(maze,  new GameLogicValidator(), new GameEngine(), new PlayerInput()) {HasWon = true};
            level.PlayLevel();
            Assert.True(level.HasWon);
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var maze = MazeSetUp();

            var level = new Level(maze, new GameLogicValidator(), new GameEngine(), new PlayerInput());
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}