using System.Text;
using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.windowsfeature.Tasks
{
    public class UninstallWindowsFeatureTask : BaseTask
    {
        private const string WINDOWS_FEATURE_MGMT_TOOLS_ARGS_KEY = "include-management-tools";
        private const string WINDOWS_FEATURE_NAME_ARGS_KEY = "name";
        public const string TASK_NAME = "uninstallfeature";

        public UninstallWindowsFeatureTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var commandStringBuilder = new StringBuilder();
            var featureName = ParsedArgs[WINDOWS_FEATURE_NAME_ARGS_KEY];
            commandStringBuilder.Append($"Uninstall-WindowsFeature -Name {featureName}");
            if (ShouldIncludeMgmt())
            {
                commandStringBuilder.Append(" -IncludeManagementTools");
            }

            return commandStringBuilder.ToString();
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var featureName = ParsedArgs[WINDOWS_FEATURE_NAME_ARGS_KEY];
            return new BaseTaskResult(result.Success, $"Feature {featureName} was uninstalled");
        }

        private bool ShouldIncludeMgmt()
        {
            return ParsedArgs.ContainsKey(WINDOWS_FEATURE_MGMT_TOOLS_ARGS_KEY) && ParsedArgs[WINDOWS_FEATURE_MGMT_TOOLS_ARGS_KEY] == "true";
        }
    }
}