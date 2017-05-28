using smartadmiral.common;
using smartadmiral.common.Tasks;

namespace smartadmiral.files.Tasks
{
    public class CopyFileFromLocalTask : ITask
    {
        private readonly string _args;
        public const string TASK_NAME = "copyfilelocal";

        public CopyFileFromLocalTask(string args, AdmiralContext context)
        {
            Context = context;
            _args = args;
        }

        public string TaskName => TASK_NAME;

        public AdmiralContext Context { get; }

        public ITaskResult Execute(IPowerShellConnection powerShellConnection)
        {
            var parsedArgs = ArgsParser.Parse(_args);
            var src = parsedArgs["localpath"];
            var dest = parsedArgs["destpath"];

            if (FileCopier.Copy(src, dest, Context))
            {
                return new BaseTaskResult(true, "Files were copied");
            }
            return new BaseTaskResult(false, "Could not copy files");
        }
    }
}
