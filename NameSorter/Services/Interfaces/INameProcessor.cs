
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    public interface INameProcessor
    {
        IEnumerable<string> SortNames(IEnumerable<string> names);
    }
}
