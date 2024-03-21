using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public interface INameReader
    {
        IEnumerable<string> ReadFromFile(string filePath);
    }


}
