using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contract;

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