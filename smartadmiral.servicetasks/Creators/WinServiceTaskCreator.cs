using smartadmiral.common;
using smartadmiral.common.Tasks;
using smartadmiral.winservice.Tasks;

namespace smartadmiral.winservice.Creators
{
    public class WinServiceTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case ListServiceTask.TASK_NAME:
                    return new ListServiceTask();
                case StartServiceTask.TASK_NAME:
                    return new StartServiceTask(args, context);
                case StopServiceTask.TASK_NAME:
                    return new StopServiceTask(args, context);
                case RestartServiceTask.TASK_NAME:
                    return new RestartServiceTask(args, context);
                case SetServiceModeTask.TASK_NAME:
                    return new SetServiceModeTask(args, context);
                default:
                    return null;
            }
        }
    }
}