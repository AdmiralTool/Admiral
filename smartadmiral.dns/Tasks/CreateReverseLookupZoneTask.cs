using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreateReverseLookupZoneTask : BaseTask
    {
        public CreateReverseLookupZoneTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createreverselookup";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var networkId = ParsedArgs["networkid"];
            var zoneFile = ParsedArgs["zonefile"];

            return $"Add-DnsServerPrimaryZone -NetworkID {networkId} -ZoneFile {zoneFile}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"Reverse lookup DNS zone for id {ParsedArgs["networkid"]} was created");
        }
    }
}