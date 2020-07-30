using System;
using Pacman;
using Pacman.TileTypes;
using Xunit;

namespace PacmanTests
{
    public class ParsingTest
    {
        
        [Fact]
        public void GetPelletFromString()
        {
            var inputData  = Parser.GetTileType('.');
            Assert.Equal(new PelletTile().Display, inputData.Display);
        }
        
        [Fact]
        public void GetWallFromString()
        {
            var inputData  = Parser.GetTileType('*');
            Assert.Equal(new WallTile().Display, inputData.Display);
        }

        [Fact]
        public void IfEmptyThrowsNewException()
        {
            Assert.Throws<Exception>(() => Parser.GetTileType(' '));
        }
    }
}