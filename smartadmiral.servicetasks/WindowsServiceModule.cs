using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.winservice.Creators;

namespace smartadmiral.winservice
{
    public class WindowsServiceModule : IModule
    {
        public const string MODULE_NAME = "service";
        private readonly WinServiceTaskCreator _taskCreator;

        public WindowsServiceModule()
        {
            _taskCreator = new WinServiceTaskCreator();
        }

        public string Name => MODULE_NAME;

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _taskCreator.Create(taskName, args, context);
        }
    }
}