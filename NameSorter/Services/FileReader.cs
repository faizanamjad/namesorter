namespace NameSorter
{
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadAllLines(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) is not null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    yield return line;
                }
            }
        }
    }
}
