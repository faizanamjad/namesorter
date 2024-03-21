using Microsoft.Extensions.Configuration;
using NameSorter;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public class ConsoleWriter : INameWriter
    {

        public void Write(IEnumerable<string> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null.");
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}








