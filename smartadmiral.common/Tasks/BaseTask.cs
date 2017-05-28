using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;

namespace smartadmiral.common.Tasks
{
    public abstract class BaseTask : ITask
    {
        protected Dictionary<string, string> ParsedArgs { get; } = new Dictionary<string, string>();

        protected BaseTask()
        {
            
        }

        protected BaseTask(string args, AdmiralContext context)
        {
            ParsedArgs = ArgsParser.Parse(args);
            Context = context;
        }

        public abstract string TaskName { get; }
        public AdmiralContext Context { get; }

        protected abstract string CreateCommand();

        protected abstract ITaskResult HandleSuccess(PowerShellResult result);

        protected virtual ITaskResult HandleError(PowerShellResult result)
        {
            var resultStringBuilder = new StringBuilder();
            resultStringBuilder.AppendLine($"Error during task execution:");
            result.Errors.ForEach(e => resultStringBuilder.AppendLine(e));
            return new BaseTaskResult(result.Success, resultStringBuilder.ToString());
        }

        public ITaskResult Execute(IPowerShellConnection powerShellConnection)
        {
            var commandToExecute = CreateCommand();
            var powerShellScriptBuilder = powerShellConnection.CreateScriptBuilder();
            var result = powerShellScriptBuilder.AddScript(commandToExecute).Invoke();
            if (result.Success)
            {
                return HandleSuccess(result);
            }
            return HandleError(result);
        }
    }
}