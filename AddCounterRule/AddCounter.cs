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
            int lastIndex = origin.LastIndexOf('.');
            var name = origin.Substring(0, lastIndex);
            var ext = origin.Substring(lastIndex + 1);

            string result = $"{name} {CurrentValue.ToString($"D{NumberOfDigit}")}.{ext}";
            CurrentValue += Step;
            return result;
        }
    }
}