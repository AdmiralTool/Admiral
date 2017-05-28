using System.Linq;
using System.Text;
using smartadmiral.common.Tasks;

namespace smartadmiral.winservice.Tasks
{
    public class ListServiceTask : BaseTask
    {
        public const string TASK_NAME = "list-services";
        public override string TaskName => TASK_NAME;

        protected override string CreateCommand() => "Get-Service";

        protected override ITaskResult HandleSuccess(PowerShellResult result)
        {
            var sb = new StringBuilder();
            foreach (var obj in result.Result.Where(o => o != null))
            {
                var dynamic = (dynamic)obj;
                sb.AppendLine($"Name: {dynamic.Name};");
                sb.AppendLine($"Display Name: {dynamic.DisplayName}");
                sb.AppendLine($"Status: {dynamic.Status};");
                sb.AppendLine("---------");
            }
            return new BaseTaskResult(true, sb.ToString());
        }
    }
}