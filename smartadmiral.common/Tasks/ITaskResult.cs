namespace smartadmiral.common.Tasks
{
    public interface ITaskResult
    {
        bool Success { get; }
        string Output { get; }
    }
}