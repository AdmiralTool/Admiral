using smartadmiral.common;
using smartadmiral.common.Logger;
using smartadmiral.common.Tasks;
using smartadmiral.dns.Tasks;

namespace smartadmiral.dns.Creators
{
    public class DnsTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case CreateARecordTask.TASK_NAME:
                    return new CreateARecordTask(args, context);
                case CreateCNameRecordTask.TASK_NAME:
                    return new CreateCNameRecordTask(args, context);
                case CreateForwarderTask.TASK_NAME:
                    return new CreateForwarderTask(args, context);
                case CreatePrimaryZoneTask.TASK_NAME:
                    return new CreatePrimaryZoneTask(args, context);
                case CreatePtrRecordTask.TASK_NAME:
                    return new CreatePtrRecordTask(args, context);
                case CreateReverseLookupZoneTask.TASK_NAME:
                    return new CreateReverseLookupZoneTask(args, context);
                case CreateSecondaryZoneTask.TASK_NAME:
                    return new CreateSecondaryZoneTask(args, context);
                case RemoveZoneTask.TASK_NAME:
                    return new RemoveZoneTask(args, context);
                default:
                {
                    Logger.Log.Error($"There was no task with name {taskName}");
                    return null;
                }
            }
        }
    }
}