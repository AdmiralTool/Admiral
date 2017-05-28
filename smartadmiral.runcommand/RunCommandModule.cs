using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.runcommand.Creators;

namespace smartadmiral.runcommand
{
    public class RunCommandModule : IModule
    {
        public const string MODULE_NAME = "command";
        private readonly RunCommandTaskCreator _taskCreator;

        public RunCommandModule()
        {
            _taskCreator = new RunCommandTaskCreator();
        }

        public string Name => MODULE_NAME;

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _taskCreator.Create(taskName, args, context);
        }
    }
}