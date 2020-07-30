using Moq;
using Pacman.Enums;
using Pacman.Interfaces;
using Pacman.Sprites;
using Pacman.TileTypes;
using Xunit;

namespace PacmanTests
{
    public class GhostTest
    {
        [Fact]
        public void GivenRandomDirectionShouldChangeCorrectly()
        {
            var mockRandom = new Mock<ISpriteBehaviour>();
            mockRandom.Setup(m => m.GetNewDirection(Direction.Up)).Returns(Direction.Up);

            var ghost = new Sprite(0,0, mockRandom.Object);
            Assert.Equal(Direction.Up,ghost.CurrentDirection);
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

            var randomGhostBehaviour = new RandomGhostBehaviour(new GhostTile()){Rng = mockRandom.Object};
            Assert.Equal(expectedDirection,randomGhostBehaviour.GetNewDirection(Direction.Down));
        }
    }
}