namespace Contract
{
    public interface IRule
    {
        string Rename(string origin);
        IRule? Parse(string data);
        string Name { get; }
    }
}