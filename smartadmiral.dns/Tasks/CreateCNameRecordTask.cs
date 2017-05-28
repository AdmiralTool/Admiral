using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreateCNameRecordTask : BaseTask
    {
        public CreateCNameRecordTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createcnamerecord";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var name = ParsedArgs["name"];
            var hostalias = ParsedArgs["hostalias"];
            var zone = ParsedArgs["zone"];

            return $"Add-DnsServerResourceRecordCName -Name {name} -HostNameAlias {hostalias} -ZoneName {zone}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"CName record for {ParsedArgs["name"]} was created");
        }
    }
}