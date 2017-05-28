using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class RemoveZoneTask : BaseTask
    {
        public RemoveZoneTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "removezone";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var zoneName = ParsedArgs["name"];

            return $"Remove-DnsServerZone {zoneName} -PassThru";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"DNS zone {ParsedArgs["name"]} was removed");
        }
    }
}