using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public class RuleDetail
    {
        public string Rule { get; set; }
        public string GetName()
        {
            string result = Rule.Split(" ", StringSplitOptions.None)[0];
            return result;
        }
        public string GetDescription()
        {
            string result = "";

            var details = Rule.Split(" ", StringSplitOptions.None);
            switch (details[0])
            {
                case "AddCounter":
                    result = $"Add counter: start from {details[1]} with step {details[2]}, {details[3]} digits";
                    break;
                case "AddPrefix":
                    result = $"Add prefix \"{details[1]}\" to file name";
                    break;
                case "AddSuffix":
                    result = $"Add suffix \"{details[1]}\" to file name";
                    break;
                case "ChangeExtension":
                    result = $"Change extension \"{details[1]}\" to \"{details[2]}\"";
                    break;
                case "ReplaceCharacter":
                    result = $"Replace \"{details[1]}\" to \"{details[2]}\"";
                    break;
                case "PascalCase":
                    result = "Convert filename to PascalCase";
                    break;
                case "LowerCaseNoSpace":
                    result = "Convert file name to lowercase and remove all spaces";
                    break;
                case "Trim":
                    result = "Remove all space from the beginning and the ending of the filename";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
