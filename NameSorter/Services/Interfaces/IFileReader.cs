namespace NameSorter
{
    public interface IFileReader
    {
        IEnumerable<string> ReadAllLines(string filePath);
    }
}
