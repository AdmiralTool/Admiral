using smartadmiral.common;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;
using smartadmiral.dns.Creators;

namespace smartadmiral.dns
{
    public class DnsModule : IModule
    {
        private DnsTaskCreator _taskCreator;
        public const string MODULE_NAME = "dns";
        public string Name => MODULE_NAME;

        public DnsModule()
        {
            _taskCreator = new DnsTaskCreator();
        }

        public ITask CreateTask(string taskName, string args, AdmiralContext context)
        {
            return _taskCreator.Create(taskName, args, context);
        }
    }
}