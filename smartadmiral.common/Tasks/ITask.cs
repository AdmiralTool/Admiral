
namespace smartadmiral.common.Tasks
{
    public interface ITask
    {
        string TaskName { get; }
        AdmiralContext Context { get; }
        ITaskResult Execute(IPowerShellConnection powerShellConnection);
    }
}