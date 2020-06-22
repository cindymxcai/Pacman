using System.IO;

namespace Pacman
{
    public class FileReader : IFileReader
    {
        public string[] ReadFile(string fileName)
        {
            var readValues = File.ReadAllLines(fileName);
            return readValues;
        }
    }
}