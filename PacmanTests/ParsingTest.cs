using System;
using Pacman;
using Pacman.Enums;
using Xunit;

namespace PacmanTests
{
    public class ParsingTest
    {
        
        [Theory]
        [InlineData('*', TileType.Wall)]
        [InlineData('.', TileType.Pellet)]
        public void GetFieldFromString(char input, TileType tileType)
        {
            var inputData  = Parser.GetTileType(input);
            Assert.Equal(tileType,inputData);
        }

        [Fact]
        public void IfEmptyThrowsNewException()
        {
            Assert.Throws<Exception>(() => Parser.GetTileType(' '));
        }
    }
}