using NameSorter;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public class NameReader : INameReader
    {
        private readonly IFileReader fileReader;

        public NameReader(IFileReader fileReader)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public IEnumerable<string> ReadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            return fileReader.ReadAllLines(filePath);
        }
    }
}
