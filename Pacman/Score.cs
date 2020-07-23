namespace Pacman
{
    public static class Score
    {
        public static int GetTotal(int remainingPellets)
        {
            return remainingPellets * 10;
        }
    }
}