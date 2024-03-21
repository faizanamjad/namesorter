using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NameSorter
{
    public class NameProcessor : INameProcessor
    {
        public IEnumerable<string> SortNames(IEnumerable<string> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null.");

            var parsedNames = names.Select(name => new Name(name));
            return Sort(parsedNames);
        }

        private IEnumerable<string> Sort(IEnumerable<Name> parsedNames)
        {
            return parsedNames
                       .OrderBy(name => name.LastName)
                       .ThenBy(name => string.Join(" ", name.GivenNames))
                       .Select(name => name.ToString());
        }



    }
}
