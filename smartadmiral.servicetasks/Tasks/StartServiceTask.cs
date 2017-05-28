using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.winservice.Tasks
{
    public class StartServiceTask : BaseTask
    {
        public const string TASK_NAME = "start-service";
        public override string TaskName => TASK_NAME;

        public StartServiceTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        protected override string CreateCommand()
        {
            var serviceName = ParsedArgs["servicename"];
            return $"Start-Service {serviceName}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var serviceName = ParsedArgs["servicename"];
            return new BaseTaskResult(true, $"{serviceName} was started");
        }
    }
}