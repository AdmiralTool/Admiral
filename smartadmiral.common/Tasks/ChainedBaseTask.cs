using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;

namespace smartadmiral.common.Tasks
{
    public abstract class ChainedBaseTask : ITask
    {
        protected Dictionary<string, string> ParsedArgs { get; }

        protected ChainedBaseTask(string args, AdmiralContext context)
        {
            ParsedArgs = ArgsParser.Parse(args);
            Context = context;
        }

        public abstract string TaskName { get; }

        public AdmiralContext Context { get; }

        protected abstract IEnumerable<string> CreateCommands();

        protected abstract void HandleIntermediateSuccess(PowerShellResult result);

        protected abstract ITaskResult HandleSuccess();

        protected virtual ITaskResult HandleError(PowerShellResult result)
        {
            var resultStringBuilder = new StringBuilder();
            resultStringBuilder.AppendLine($"Error during task execution:");
            result.Errors.ForEach(e => resultStringBuilder.AppendLine(e));
            return new BaseTaskResult(result.Success, resultStringBuilder.ToString());
        }

        public ITaskResult Execute(IPowerShellConnection powerShellConnection)
        {
            var commandsToExecute = CreateCommands().Where(x => !string.IsNullOrEmpty(x));

            foreach (var command in commandsToExecute)
            {
                var powerShellScriptBuilder = powerShellConnection.CreateScriptBuilder();
                var result = powerShellScriptBuilder.AddScript(command).Invoke();
                if (!result.Success)
                {
                    return HandleError(result);
                }
                HandleIntermediateSuccess(result);
            }
            return HandleSuccess();
        }
    }
}