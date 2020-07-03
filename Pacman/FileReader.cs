using System.IO;

namespace Pacman
{
    public class FileReader : IFileReader
    {
        public string[] ReadFile(string fileName)
        {
           return File.ReadAllLines(fileName);
        
        }

        public string ReadAll(string fileName)
        {
            return File.ReadAllText(fileName);
        }
    }
}