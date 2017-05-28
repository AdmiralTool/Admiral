using System.Collections.Generic;
using System.Linq;
using smartadmiral.common;
using smartadmiral.common.Directives;
using smartadmiral.common.Modules;
using smartadmiral.common.Tasks;

namespace smartadmiral.console.Core
{
    internal class TaskCreator
    {
        private readonly IEnumerable<IModule> _modules;

        public TaskCreator(params IModule[] modules)
        {
            _modules = modules;
        }

        public virtual ITask CreateTask(TaskDescription taskDescription, AdmiralContext context)
        {
            var module = _modules.FirstOrDefault(x => x.Name == taskDescription.Module);
            return module?.CreateTask(taskDescription.TaskName, taskDescription.Args, context);
        }
    }
}