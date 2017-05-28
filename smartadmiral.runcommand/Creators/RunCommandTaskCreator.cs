using smartadmiral.common;
using smartadmiral.common.Tasks;
using smartadmiral.runcommand.Tasks;

namespace smartadmiral.runcommand.Creators
{
    public class RunCommandTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case RunCommandTask.TASK_NAME:
                    return new RunCommandTask(args);
                case RunScriptTask.TASK_NAME:
                    return new RunScriptTask(args, context);
                default:
                    return null;
            }
        }
    }
}