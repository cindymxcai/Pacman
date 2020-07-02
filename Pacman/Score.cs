namespace Pacman
{
    public static class Score
    {
        public static int GetTotal(int totalPellets, int remainingPellets)
        {
            return (totalPellets - remainingPellets) * 10;
        }
    }
}