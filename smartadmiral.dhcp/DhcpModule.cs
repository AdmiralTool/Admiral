using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.dhcp.Creators;

namespace smartadmiral.dhcp
{
    public class DhcpModule : IModule
    {
        private DhcpTaskCreator _creator;
        public const string MODULE_NAME = "dhcp";
        public string Name => MODULE_NAME;

        public DhcpModule()
        {
            _creator = new DhcpTaskCreator();
        }

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _creator.Create(taskName, args, context);
        }
    }
}