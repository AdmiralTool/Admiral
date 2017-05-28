namespace smartadmiral.common.Tasks
{
    public class BaseTaskResult : ITaskResult
    {
        public BaseTaskResult(bool success, string output)
        {
            Success = success;
            Output = output;
        }

        public bool Success { get; }
        public string Output { get; }
    }
}