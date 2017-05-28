using System.Text;
using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreateARecordTask : BaseTask
    {
        public CreateARecordTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createarecord";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var host = ParsedArgs["host"];
            var zoneName = ParsedArgs["zone"];
            var ip = ParsedArgs["ip"];
            var ttl = ParsedArgs["ttl"];

            var commandBuilder = new StringBuilder($"Add-DnsServerResourceRecordA -Name {host} -ZoneName {zoneName} -IPv4Address {ip} -TimeToLive {ttl}");

            if (ArgsParser.IsFeatureAllowed(ParsedArgs, "createptr"))
            {
                commandBuilder.Append(" -CreatePtr");
            }

            return commandBuilder.ToString();
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"ARecord for {ParsedArgs["host"]} was created");
        }
    }
}