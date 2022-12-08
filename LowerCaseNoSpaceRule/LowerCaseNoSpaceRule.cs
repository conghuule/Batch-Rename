using Contract;
using System.Text;

namespace LowerCaseNoSpaceRule
{
    public class LowerCaseNoSpaceRule : IRule
    {
        public string Name => "LowerCaseNoSpace";

        public IRule? Parse(string data)
        {
            return new LowerCaseNoSpaceRule();
        }

        public string Rename(string origin)
        {
            StringBuilder builder = new StringBuilder();
            string lowerCaseOrigin = origin.ToLower();
            for (int i = 0; i < origin.Length; i++)
            {
                if (lowerCaseOrigin[i] != ' ')
                {
                    builder.Append(lowerCaseOrigin[i]);
                }
            }
            string result = builder.ToString();
            return result;
        }
    }
}