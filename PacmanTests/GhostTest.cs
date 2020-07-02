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
    public class GhostTest
    {
        [Fact]
        public void GivenRandomDirectionShouldChangeCorrectly()
        {
            var mockRandom = new Mock<ISpriteBehaviour>();
            mockRandom.Setup(m => m.ChooseDirection()).Returns(Direction.Right);

            var ghost = new Sprite(0,0, mockRandom.Object);
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
            var mockRandom = new Mock<ISpriteBehaviour>();
            mockRandom.Setup(m => m.ChooseDirection()).Returns(newDirection);
            
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "LevelSettings.json");
            var json = File.ReadAllText(jsonFileName);
            var levels = JsonConvert.DeserializeObject<LevelData>(json);
            var fileReader = new FileReader();
            var mazeData = fileReader.ReadFile(levels.levels[1]);
            var maze = new Maze(mazeData);
            var spriteFactory = new SpriteFactory();
            var level = new Level( spriteFactory,  new GameLogicValidator(), new GameEngine(), new PlayerInput(), new PacmanBehaviour(), new RandomGhostBehaviour())
                {Ghosts = { new Sprite(4, 5, mockRandom.Object)}};
            var gameEngine = new GameEngine();

            var (x, y) = gameEngine.GetNewPosition(level.Ghosts[2], maze);
            level.Ghosts[2].SetNewPosition(x, y);
            Assert.Equal(newX, level.Ghosts[2].X);
            Assert.Equal(newY, level.Ghosts[2].Y);
        }
    }
}