using Contract;
using System.Text.RegularExpressions;

namespace PascalCaseRule
{
    public class PascalCaseRule : IRule
    {
        public string Name => "PascalCase";

        public IRule? Parse(string data)
        {
            return new PascalCaseRule();
        }

        string IRule.Rename(string origin)
        {
            List<string> wordList = new List<string>(); 
            var words = origin.Split(" ", StringSplitOptions.None);
            foreach (string word in words) {
                string newWord = char.ToUpper(word[0]) + word.Substring(1);
                wordList.Add(newWord);
            }

            return String.Join(" ", wordList.ToArray());
        }
    }
}