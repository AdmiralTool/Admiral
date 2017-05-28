using System.Text;
using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreatePrimaryZoneTask : BaseTask
    {
        public CreatePrimaryZoneTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createprimaryzone";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var zoneName = ParsedArgs["name"];
            var zoneFile = ParsedArgs["zonefile"];
            var allowDynamicUpdate = ArgsParser.IsFeatureAllowed(ParsedArgs, "dynamicUpdate");

            var commandBuilder =
                new StringBuilder(
                    $"Add-DnsServerPrimaryZone -Name {zoneName} -ZoneFile {zoneFile}");

            if (allowDynamicUpdate)
                commandBuilder.Append(" -DynamicUpdate NonsecureAndSecure");

            commandBuilder.Append(" -PassThru");

            return commandBuilder.ToString();
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"DNS primary zone {ParsedArgs["name"]} was created");
        }
    }
}
