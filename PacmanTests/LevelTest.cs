using Moq;
using Pacman;
using Pacman.Enums;
using Pacman.Sprites;
using Xunit;

namespace PacmanTests
{
    public class GameTest
    {
        [Fact]
        public void ScoreShouldInitiallyBe0()
        {
            var level = new Level(1, new FileReader());
            Assert.Equal(0, level.Score);
        }
        
        [Fact]
        public void LivesLeftShouldStartAt3()
        {
            var level = new Level(1, new FileReader());
            Assert.Equal(3, level.LivesLeft);
        }
        
        [Theory]
        [InlineData( 2, TileType.PacmanRight)]
        [InlineData( 1, TileType.PacmanChomp)]
        public void UpdatesMazeWithCorrectTile(int counter, TileType tileType)
        {
            var level = new Level( 2, new FileReader());
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
            var level = new Level(1, new FileReader()) {Pacman = new PacmanSprite(0, 0, Direction.Down)};
            var mockGhost = new Mock<IGhostBehaviour>();
            mockGhost.Setup(ghostBehaviour => ghostBehaviour.ChooseDirection()).Returns(Direction.Left);
            level.Ghosts[1] = new GhostSprite(0,0, mockGhost.Object);
            Assert.True(level.GameLogicValidator.HasCollidedWithGhost(level.Pacman, level.Ghosts));
        }

        [Fact]
        public void HasEatenAllPelletsIfRemainingPelletsEqualsZero()
        {
            var level = new Level(1, new FileReader());
            Assert.True(level.GameLogicValidator.HasEatenAllPellets(0));
            Assert.False(level.GameLogicValidator.HasEatenAllPellets(2));
        }
        
        [Fact]
        public void HasNotWonLevelIfLivesLeftIs0()
        {
            var level = new Level(1, new FileReader()) {LivesLeft = 0};
            Assert.False(level.HasWon);
        }
        
        [Fact]
        public void HasWonLevelIfLivesLeftIsNot0AndHasWon()
        {
            var level = new Level(1, new FileReader()) {HasWon = true};
            Assert.True(level.HasWonLevel());
        }
        
        [Fact]
        public void HandlesDeathIfGhostCollision()
        {
            var level = new Level(1, new FileReader());
            level.HandleDeath();
            Assert.Equal(2, level.LivesLeft);
            Assert.Equal(1, level.Pacman.X);
            Assert.Equal(1, level.Pacman.Y);
        }
    }
}