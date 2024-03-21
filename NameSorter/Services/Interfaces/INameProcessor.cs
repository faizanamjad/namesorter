
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter
{
    public interface INameProcessor
    {
        List<string> SortNames(IEnumerable<string> names);
    }
}
