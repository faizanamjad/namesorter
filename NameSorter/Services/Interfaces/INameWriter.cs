using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter
{
    public interface INameWriter
    {
        void Write(IEnumerable<string> names);
    }


}
