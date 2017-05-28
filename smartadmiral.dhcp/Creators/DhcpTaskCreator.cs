using smartadmiral.common;
using smartadmiral.common.Logger;
using smartadmiral.common.Tasks;
using smartadmiral.dhcp.Tasks;

namespace smartadmiral.dhcp.Creators
{
    public class DhcpTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case CreateNewPoolTask.TASK_NAME:
                    return new CreateNewPoolTask(args, context);
                default:
                {
                    Logger.Log.Error($"There was no task with name {taskName}");
                    return null;
                }
            }
        }
    }
}