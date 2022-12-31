using Contract;

namespace AddSuffixRule
{
    public class AddSuffix : IRule
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
            int lastIndex = origin.LastIndexOf('.');
            var name = origin.Substring(0, lastIndex);
            var ext = origin.Substring(lastIndex + 1);

            string newName = $"{name} {Suffix}.{ext}";
            return newName;
        }
    }
}