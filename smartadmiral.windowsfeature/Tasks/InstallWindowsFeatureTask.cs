using System.Text;
using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.windowsfeature.Tasks
{
    public class InstallWindowsFeatureTask : BaseTask
    {
        public InstallWindowsFeatureTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        private const string WINDOWS_FEATURE_NAME_ARGS_KEY = "name";

        private const string WINDOWS_FEATURE_SUB_FEATURES_ARGS_KEY = "include-sub-features";

        private const string WINDOWS_FEATURE_MGMT_TOOLS_ARGS_KEY = "include-management-tools";

        public const string TASK_NAME = "installfeature";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var commandStringBuilder = new StringBuilder();
            var featureName = ParsedArgs[WINDOWS_FEATURE_NAME_ARGS_KEY];

            commandStringBuilder.Append($"Install-WindowsFeature -Name {featureName}");

            if (ArgsParser.IsFeatureAllowed(ParsedArgs, WINDOWS_FEATURE_SUB_FEATURES_ARGS_KEY))
            {
                commandStringBuilder.Append(" -IncludeAllSubFeature");
            }
            if (ArgsParser.IsFeatureAllowed(ParsedArgs, WINDOWS_FEATURE_MGMT_TOOLS_ARGS_KEY))
            {
                commandStringBuilder.Append(" -IncludeManagementTools");
            }

            return commandStringBuilder.ToString();
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var featureName = ParsedArgs[WINDOWS_FEATURE_NAME_ARGS_KEY];
            return new BaseTaskResult(result.Success, $"Feature {featureName} was installed");
        }
    }
}
