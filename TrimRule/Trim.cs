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

        public FileInfor Rename(FileInfor origin)
        {
            origin.Filename = origin.Filename.Trim();
            return origin;
        }
    }
}