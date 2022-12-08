using System;
using Contract;

namespace AddCounterRule
{
    public class AddCounter : IRule
    {
        public int StartValue { get; set; }
        public int Step { get; set; }
        public int NumberOfDigit { get; set; }
        static public int CurrentValue { get; set; }
        public string Name => "AddCounter";

        public IRule? Parse(string data)
        {
            const string Space = " ";
            var tokens = data.Split(Space, StringSplitOptions.None);

            int startValue = Int32.Parse(tokens[1]);
            int step = Int32.Parse(tokens[2]);
            int numberOfDigit = Int32.Parse(tokens[3]);

            AddCounter result = new AddCounter();
            result.StartValue = startValue;
            result.Step = step;
            result.NumberOfDigit = numberOfDigit;
            CurrentValue = result.StartValue;
            return result;
        }

        public string Rename(string origin)
        {
            string result = $"{origin} {CurrentValue.ToString("D3")}";
            CurrentValue += Step;
            return result;
        }
    }
}