using Moq;
using Pacman;
using PacmanTests;
using Pacman.Enums;
using Pacman.Sprites;
using Xunit;

namespace PacmanTests
{
    public class GhostTest
    {
        [Fact]
        public void GivenRandomDirectionShouldChangeCorrectly()
        {
            var mockRandom = new Mock<IGhostBehaviour>();
            mockRandom.Setup(m => m.ChooseDirection()).Returns(Direction.Right);

            var ghost = new GhostSprite(0,0, mockRandom.Object);
            Assert.Equal(Direction.Right,ghost.CurrentDirection);
        }

        [Theory]
        [InlineData(0, Direction.Up)]        
        [InlineData(1, Direction.Down)]
        [InlineData(2, Direction.Left)]
        [InlineData(3, Direction.Right)]

        public void ChooseDirectionShouldReturnCorrectDirection(int randomNumber, Direction expectedDirection)
        {
            var mockRandom = new Mock<IRng>();
            mockRandom.Setup(m => m.Next(0,4)).Returns(randomNumber);

            var randomGhostBehaviour = new RandomGhostBehaviour{Rng = mockRandom.Object};
            Assert.Equal(expectedDirection,randomGhostBehaviour.ChooseDirection());
        }
        
        [Theory]
        [InlineData(Direction.Up, 3, 5 )]
        [InlineData(Direction.Down, 5, 5 )]
        [InlineData(Direction.Left, 4, 4 )]
        [InlineData(Direction.Right, 4, 6 )]

        public void ShouldContinueToMoveInCurrentDirection(Direction newDirection, int newX, int newY )
        {
            var mockRandom = new Mock<IGhostBehaviour>();
            mockRandom.Setup(m => m.ChooseDirection()).Returns(newDirection);
            var level = new Level( 1, new FileReader())
                {Ghosts = { new GhostSprite(4, 5, mockRandom.Object)}};
            var (x, y) = GameEngine.GetNewPosition(level.Ghosts[2], level.GameMaze);
            level.Ghosts[2].SetNewPosition(x, y);
            Assert.Equal(newX, level.Ghosts[2].X);
            Assert.Equal(newY, level.Ghosts[2].Y);
        }
    }
}