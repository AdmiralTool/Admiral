using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.winservice.Tasks
{
    public class StopServiceTask : BaseTask
    {
        public const string TASK_NAME = "stop-service";
        public override string TaskName => TASK_NAME;

        public StopServiceTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        protected override string CreateCommand()
        {
            var serviceName = ParsedArgs["servicename"];
            return $"Stop-Service {serviceName}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var serviceName = ParsedArgs["servicename"];
            return new BaseTaskResult(true, $"{serviceName} was stopped");
        }
    }
}