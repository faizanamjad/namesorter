namespace NameSorter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class Name
    {
        public string LastName { get; }
        public string[] GivenNames { get; }

        public Name(string fullName)
        {
            var nameParts = fullName.Trim().Split(' ');
            ValidateNameParts(nameParts);

            LastName = nameParts[^1];
            GivenNames = nameParts[..^1];
        }

        public override string ToString() => $"{string.Join(" ", GivenNames)} {LastName}";

        private void ValidateNameParts(string[] nameParts)
        {
            if (nameParts.Length < 2 || nameParts.Length > 4)
                throw new ArgumentException($"The provided name '{string.Join(" ", nameParts)}' must consist of 2 to 4 parts, comprising either 1 or 3 given names and 1 last name.");
        }
    }

}





