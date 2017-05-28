using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.winservice.Tasks
{
    public class RestartServiceTask : BaseTask
    {
        public RestartServiceTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "restart-service";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var serviceName = ParsedArgs["servicename"];
            return $"Restart-Service {serviceName}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var serviceName = ParsedArgs["servicename"];
            return new BaseTaskResult(true, $"{serviceName} was restarted");
        }
    }
}