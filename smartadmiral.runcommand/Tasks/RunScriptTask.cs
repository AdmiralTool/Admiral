using System.IO;
using System.Text;
using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.runcommand.Tasks
{
    public class RunScriptTask : BaseTask
    {
        public RunScriptTask(string args, AdmiralContext context) : base(args, context)
        {
        }

        public const string TASK_NAME = "powershell-script";

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            var fileName = ParsedArgs["path"];
            var fileContent = File.ReadAllText(fileName);
            return fileContent;
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var resultStringBuilder = new StringBuilder();
            foreach (var psObject in result.Result)
            {
                resultStringBuilder.AppendLine(psObject.ToString());
            }
            return new BaseTaskResult(true, resultStringBuilder.ToString());
        }
    }
}