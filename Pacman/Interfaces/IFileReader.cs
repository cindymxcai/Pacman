namespace Pacman.Interfaces
{
    public interface IFileReader
    {
        string[] ReadFile(string fileName);
        string ReadAllData(string fileName);
    }
}