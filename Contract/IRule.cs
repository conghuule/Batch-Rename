namespace Contract
{
    public interface IRule
    {
        FileInfor Rename(FileInfor origin);
        IRule? Parse(string data);
        string Name { get; }
    }
}