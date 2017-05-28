namespace smartadmiral.common.Directives
{
    public class TaskDescription
    {
        public TaskDescription(string module, string taskName, string args)
        {
            Module = module;
            TaskName = taskName;
            Args = args;
        }

        public string Module { get; }
        public string TaskName { get; }
        public string Args { get; }
    }
}