using System;
using Pacman.Enums;

namespace Pacman.Sprites
{
    public class RandomGhostBehaviour : ISpriteBehaviour
    {
        public IRng Rng;

        public RandomGhostBehaviour()
        {
            Rng = new Rng();
        }

        public Direction ChooseDirection()
        {
            var direction = Rng.Next(0, 4) switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
                _ => throw new Exception()
            };
            return direction;
        }
    }
}