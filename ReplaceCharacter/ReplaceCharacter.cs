using Contract;
using System.Text;

namespace ReplaceCharacter
{
    public class ReplaceCharacter : IRule
    {
        List<string> oldWord = new List<string>();
        List<string> newWord = new List<string>();
        int replaceLength = 0;
        string IRule.Name => "ReplaceCharacter";

        public IRule? Parse(string data)
        {
            List<string> oldWord = new List<string>();
            List<string> newWord = new List<string>();
            const string Space = " ";
            var tokens = data.Split(
                new string[] { Space }, StringSplitOptions.None);
            for (int i = 1; i < tokens.Length; i++)
            {
                if (i <= tokens.Length / 2)
                {
                    oldWord.Add(tokens[i]);
                }
                else
                {
                    newWord.Add(tokens[i]);
                }
            }
            int replaceLength = (tokens.Length - 1) / 2;

            ReplaceCharacter result = new ReplaceCharacter();
            result.replaceLength = replaceLength;
            result.oldWord = oldWord;
            result.newWord = newWord;

            return result;
        }

        string IRule.Rename(string origin)
        {
            StringBuilder builder = new StringBuilder(origin);
            for (int i = 0; i < replaceLength; i++)
            {
                builder.Replace($"{oldWord[i]}", $"{newWord[i]}");

            }
            string result = builder.ToString();
            return result;
        }
    }
}