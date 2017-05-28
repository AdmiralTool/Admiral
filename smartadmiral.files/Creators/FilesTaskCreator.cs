using smartadmiral.common;
using smartadmiral.common.Tasks;
using smartadmiral.files.Tasks;

namespace smartadmiral.files.Creators
{
    public class FilesTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case CopyFileFromLocalTask.TASK_NAME:
                    return new CopyFileFromLocalTask(args, context);
                default:
                    return null;
            }
        }
    }
}