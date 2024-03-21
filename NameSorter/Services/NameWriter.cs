using Microsoft.Extensions.Configuration;
using NameSorter;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public class NameWriter : INameWriter
    {
        private readonly IEnumerable<INameWriter> writers;

        public NameWriter(IEnumerable<INameWriter> writers)
        {
            this.writers = writers ?? throw new ArgumentNullException(nameof(writers));
        }

        public void Write(IEnumerable<string> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentNullException("Names collection cannot be null.", nameof(names));

            foreach (var writer in writers)
            {
                writer.Write(names);
            }
        }
    }
}








