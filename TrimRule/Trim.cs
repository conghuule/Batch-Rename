using Contract;

namespace TrimRule
{
    public class Trim : IRule
    {
        public string Name => "Trim";

        public IRule? Parse(string data)
        {
            return new Trim();
        }

        public string Rename(string origin)
        {
            string result = origin.Trim();
            return result;
        }
    }
}