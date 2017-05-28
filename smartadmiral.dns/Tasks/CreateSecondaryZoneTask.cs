using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreateSecondaryZoneTask : BaseTask
    {
        public CreateSecondaryZoneTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createsecondaryzone";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var zoneName = ParsedArgs["name"];
            var zoneFile = ParsedArgs["zonefile"];
            var masterIp = ParsedArgs["master"];

            return $"Add-DnsServerSecondaryZone -Name {zoneName} -ZoneFile {zoneFile} -MasterServers {masterIp} -PassThru";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"DNS secondary zone {ParsedArgs["name"]} was created");
        }
    }
}