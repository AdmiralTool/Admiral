using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreateForwarderTask : BaseTask
    {
        public CreateForwarderTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createforwarder";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var ip = ParsedArgs["ip"];

            return $"Add-DnsServerForwarder -IPAddress {ip} -PassThru";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"DNS Forwarder for {ParsedArgs["ip"]} was created");
        }
    }
}