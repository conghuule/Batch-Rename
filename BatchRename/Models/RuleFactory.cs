using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename.Models
{
    public class RuleFactory
    {
        static Dictionary<string, IRule> _prototypes = new Dictionary<string, IRule>();

        public static void Register(IRule prototype)
        {
            _prototypes.Add(prototype.Name, prototype);
        }

        private static RuleFactory? _instance = null;
        public static RuleFactory Instance()
        {
            if (_instance == null)
            {
                _instance = new RuleFactory();
            }
            return _instance;
        }

        public IRule? Parse(string data)
        {
            const string Space = " ";
            var tokens = data.Split(Space, StringSplitOptions.None);

            var keyword = tokens[0];
            IRule? result = null;

            if (_prototypes.ContainsKey(keyword))
            {
                IRule prototype = _prototypes[keyword];
                result = prototype.Parse(data);
            }

            return result;
        }

    }
}
