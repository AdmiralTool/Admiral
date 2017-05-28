using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.files.Creators;

namespace smartadmiral.files
{
    public class FilesModule : IModule
    {
        private readonly FilesTaskCreator _taskCreator;
        public const string MODULE_NAME = "files";

        public string Name => MODULE_NAME;

        public FilesModule()
        {
            _taskCreator = new FilesTaskCreator();
        }

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _taskCreator.Create(taskName, args, context);
        }
    }
}