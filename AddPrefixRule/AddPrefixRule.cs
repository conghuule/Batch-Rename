using Contract;

namespace AddPrefixRule
{
    public class AddPrefix : IRule
    {
        public string Prefix { get; set; }

        public string Name => "AddPrefix";

        public IRule? Parse(string data)
        {
            const string Space = " ";
            var tokens = data.Split(
                new string[] { Space }, StringSplitOptions.None
            );
            var prefix = tokens[1];

            AddPrefix result = new AddPrefix();
            result.Prefix = prefix;

            return result;
        }

        public string Rename(string origin)
        {
            string newName = $"{Prefix} {origin}";
            return newName;
        }
    }
}