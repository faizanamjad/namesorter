using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    public class App
    {
        private readonly INameReader reader;
        private readonly INameProcessor sorter;
        private readonly INameWriter writer;

        public App(INameReader reader, INameProcessor sorter, INameWriter writer)
        {
            this.reader = reader;
            this.sorter = sorter;
            this.writer = writer;
        }

        public void Run(string filePath)
        {
            var names = reader.ReadFromFile(filePath);
            var sortedNames = sorter.SortNames(names);
            writer.Write(sortedNames);
        }
    }
}
