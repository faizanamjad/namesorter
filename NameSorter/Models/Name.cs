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

            LastName = nameParts[^1];// last element
            GivenNames = nameParts[..^1];// all elements except last
        }

        public override string ToString() => $"{string.Join(" ", GivenNames)} {LastName}";

        private void ValidateNameParts(string[] nameParts)
        {
            if (nameParts.Length < 2 || nameParts.Length > 4)
                throw new ArgumentException($"Invalid name format: '{string.Join(" ", nameParts)}'. A valid name must have 2 to 4 parts, including 1 to 3 given names followed by a last name.");
        }
    }

}





