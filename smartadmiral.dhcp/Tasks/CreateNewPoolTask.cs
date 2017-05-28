using System.Collections.Generic;
using System.Text;
using smartadmiral.common;
using smartadmiral.common.Extensions;
using smartadmiral.common.Tasks;

namespace smartadmiral.dhcp.Tasks
{
    public class CreateNewPoolTask : ChainedBaseTask
    {
        public CreateNewPoolTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "createpool";
        public override string TaskName => TASK_NAME;

        protected override IEnumerable<string> CreateCommands()
        {
            var name = ParsedArgs["name"];
            var startRange = ParsedArgs["startRange"];
            var endRange = ParsedArgs["endRange"];
            var subnetMask = ParsedArgs["subnetMask"];

            yield return $"Add-DhcpServerV4Scope -Name {name} -StartRange {startRange} -EndRange {endRange} -SubnetMask {subnetMask}";

            yield return HandleV4Option();

            yield return HandleLeaseDays();
        }

        private string HandleV4Option()
        {
            var shouldSetV4OptionValue = ParsedArgs.ContainsKey("dnsserver") || ParsedArgs.ContainsKey("dnsdomain") ||
                                         ParsedArgs.ContainsKey("gateway");

            if (shouldSetV4OptionValue)
            {
                var dnsServer = ParsedArgs.TryFindValue("dnsserver");
                var dnsDomain = ParsedArgs.TryFindValue("dnsdomain");
                var gateway = ParsedArgs.TryFindValue("gateway");

                var commandBuilder = new StringBuilder("Set-DhcpServerV4OptionValue");
                if (dnsServer != null)
                {
                    commandBuilder.Append($" -DnsServer {dnsServer}");
                }
                if (dnsDomain != null)
                {
                    commandBuilder.Append($" -DnsDomain {dnsDomain}");
                }
                if (gateway != null)
                {
                    commandBuilder.Append($" -Router {gateway}");
                }

                return commandBuilder.ToString();
            }

            return string.Empty;
        }

        private string HandleLeaseDays()
        {
            var leaseDays = ParsedArgs.TryFindValue("leasedays");
            if (leaseDays != null)
            {
                return $"Set-DhcpServerv4Scope -ScopeId {Context.Hostname} -LeaseDuration (New-TimeSpan -Days {leaseDays})";
            }
            return string.Empty;
        }

        protected override void HandleIntermediateSuccess(PowerShellResult result)
        {
        }

        protected override ITaskResult HandleSuccess()
        {
            return new BaseTaskResult(true, "Dhcp pool was created sucessfully");
        }
    }
}