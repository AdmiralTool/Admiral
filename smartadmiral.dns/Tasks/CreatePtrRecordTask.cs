using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.dns.Tasks
{
    public class CreatePtrRecordTask : BaseTask
    {
        public CreatePtrRecordTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createptrrecord";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var name = ParsedArgs["name"];
            var zone = ParsedArgs["zone"];
            var ttl = ParsedArgs["ttl"];
            var ptrName = ParsedArgs["ptrname"];

            return
                $"Add-DnsServerResourceRecordPtr -Name {name} -ZoneName {zone} -TimeToLive {ttl} -PtrDomainName {ptrName}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(result.Success, $"PTR record {ParsedArgs["ptrname"]} was created");
        }
    }
}
