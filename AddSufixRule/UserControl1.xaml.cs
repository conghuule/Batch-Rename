using System;
using Contract;

namespace AddSuffixRule
{
    class AddSuffix : IRule
    {
        public string Suffix { get; set; }

        public string Name => "AddSuffix";

        public IRule? Parse(string data)
        {
            const string Space = " ";
            var tokens = data.Split(
                new string[] { Space }, StringSplitOptions.None
            );
            var Suffix = tokens[1];

            AddSuffix result = new AddSuffix();
            result.Suffix = Suffix;

            return result;
        }

        public string Rename(string origin)
        {
            string newName = $"{origin} {Suffix}";
            return newName;
        }
    }
}