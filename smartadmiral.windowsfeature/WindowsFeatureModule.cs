using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.windowsfeature.Creators;

namespace smartadmiral.windowsfeature
{
    public class WindowsFeatureModule : IModule
    {
        public const string MODULE_NAME = "winfeature";
        private readonly WindowsFeatureTaskCreator _creator;

        public WindowsFeatureModule()
        {
            _creator = new WindowsFeatureTaskCreator();
        }

        public string Name => MODULE_NAME;

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _creator.Create(taskName, args, context);
        }
    }
}