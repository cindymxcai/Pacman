namespace Pacman
{
    public static class Constants
    {
        public static int GetScore(int totalPellets, int remainingPellets)
        {
            return (totalPellets - remainingPellets) * 10;
        }
    }
}