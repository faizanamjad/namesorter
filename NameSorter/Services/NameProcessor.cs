using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NameSorter
{
    public class NameProcessor : INameProcessor
    {

        public List<string> SortNames(IEnumerable<string> names)
        {
            if (names == null || !names.Any())
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null.");

            //remove any empty strings


            var parsedNames = names.Select(name => new Name(name));
            return Sort(parsedNames);
        }

        private List<string> Sort(IEnumerable<Name> parsedNames)
        {
            return parsedNames
                       .OrderBy(name => name.LastName)
                       .ThenBy(name =>  string.Join(" ", name.GivenNames))
                       .Select(name => name.ToString())
                       .ToList();
        }



    }
}
