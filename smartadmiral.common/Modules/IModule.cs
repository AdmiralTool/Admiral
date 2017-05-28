using smartadmiral.common.Tasks;

namespace smartadmiral.common.Modules
{
    public interface IModule
    {
        string Name { get; }
        ITask CreateTask(string taskName, string args, AdmiralContext context);
    }
}