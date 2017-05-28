using smartadmiral.common;
using smartadmiral.common.Tasks;
using smartadmiral.windowsfeature.Tasks;

namespace smartadmiral.windowsfeature.Creators
{
    public class WindowsFeatureTaskCreator
    {
        public ITask Create(string taskName, string args, AdmiralContext context)
        {
            switch (taskName)
            {
                case InstallWindowsFeatureTask.TASK_NAME:
                    return new InstallWindowsFeatureTask(args, context);
                case UninstallWindowsFeatureTask.TASK_NAME:
                    return new UninstallWindowsFeatureTask(args, context);
                default:
                    return null;
            }
        }
    }
}