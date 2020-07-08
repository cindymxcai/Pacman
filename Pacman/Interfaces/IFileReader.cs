namespace Pacman
{
    public interface IFileReader
    {
        string[] ReadFile(string fileName);
        string ReadAll(string fileName);
    }
}