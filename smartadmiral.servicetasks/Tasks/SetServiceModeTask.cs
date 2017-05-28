using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.winservice.Tasks
{
    public class SetServiceModeTask : BaseTask
    {
        public SetServiceModeTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "set-service";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var serviceName = ParsedArgs["servicename"];
            var mode = ParsedArgs["mode"];
            var psMode = TranslateMode(mode);

            return $"Set-Service {serviceName} -StartupType {psMode}";
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var serviceName = ParsedArgs["servicename"];
            return new BaseTaskResult(true, $"Service {serviceName} start mode was changed to '{ParsedArgs["mode"]}'");
        }

        private string TranslateMode(string modeFromParams)
        {
            switch (modeFromParams)
            {
                case "auto":
                    return "Automatic";
                case "manual":
                    return "Manual";
                case "disabled":
                    return "Disabled";
                default:
                    return "Manual";
            }
        }
    }
}