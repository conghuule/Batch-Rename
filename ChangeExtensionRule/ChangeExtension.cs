using Contract;
using System.Diagnostics;
using System.Text;

namespace ChangeExtensionRule
{
    public class ChangeExtension : IRule
    {
        public string OldExtension { get; set; }
        public string NewExtension { get; set; }
        public string Name => "ChangeExtension";

        public IRule? Parse(string data)
        {
            const string Space = " ";
            var tokens = data.Split(Space, StringSplitOptions.None);

            string oldExtension = tokens[1];
            string newExtension = tokens[2];


            Debug.WriteLine(data);

            ChangeExtension result = new ChangeExtension();
            result.OldExtension = oldExtension;
            result.NewExtension = newExtension;

            return result;
        }

        public string Rename(string origin)
        {
            string result = origin.Replace("." + OldExtension, "." +  NewExtension);
            return result;
        }
    }
}