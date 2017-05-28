using smartadmiral.common.Tasks;

namespace smartadmiral.runcommand.Tasks
{
    public class RunCommandTask : BaseTask
    {
        private readonly string _command;
        public const string TASK_NAME = "runcommand";

        public RunCommandTask(string args)
        {
            _command = args;
        }

        public override string TaskName => TASK_NAME;

        protected override string CreateCommand()
        {
            return _command;
        }

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            return new BaseTaskResult(true, "Command ran successfully");
        }
    }
}