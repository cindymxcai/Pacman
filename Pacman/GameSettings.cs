namespace Pacman
{
    public class GameSettings
    {
        public GameSettings(string[] levelSettings, int maxLevels)
        {
            LevelSettings = levelSettings;
            MaxLevels = maxLevels;
        }

        public string[] LevelSettings { get; }
        public int MaxLevels { get; }
        
    }
}