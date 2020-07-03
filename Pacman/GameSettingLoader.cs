using System;
using System.IO;
using Newtonsoft.Json;

namespace Pacman
{
    public class GameSettingLoader : IGameSettingLoader
    {
        private readonly IFileReader _fileReader;

        public GameSettingLoader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        
        public GameSettings GetLevelData()
        {
            var jsonFileName = Path.Combine(Environment.CurrentDirectory, "GameSettings.json");
            var json = _fileReader.ReadAll(jsonFileName);
            return JsonConvert.DeserializeObject<GameSettings>(json);
        }
    }
}