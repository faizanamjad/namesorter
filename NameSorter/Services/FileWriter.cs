using Microsoft.Extensions.Configuration;
using NameSorter;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public class FileWriter : INameWriter
    {
        private readonly string _filePath;

        public FileWriter(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Output File path cannot be null or empty.", nameof(filePath));

            _filePath = filePath;
        }

        public void Write(IEnumerable<string> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null.");
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    foreach (string name in names)
                    {
                        writer.WriteLine(name);
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                throw new InvalidOperationException($"An error occurred while writing to the file: {_filePath}.", ex);
            }
        }
    }
}








