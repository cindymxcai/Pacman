namespace Pacman
{
    public class LevelData
    {
        public LevelData(string[] levels, int maxLevels)
        {
            Levels = levels;
            MaxLevels = maxLevels;
        }

        public string[] Levels { get; }
        public int MaxLevels { get; }
    }
}