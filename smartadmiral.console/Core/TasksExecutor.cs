using System;
using smartadmiral.common;
using smartadmiral.common.Directives;
using smartadmiral.common.Logger;
using smartadmiral.common.Tasks;

namespace smartadmiral.console.Core
{
    internal class TasksExecutor
    {
        private readonly TaskCreator _taskCreator;

        public TasksExecutor(TaskCreator taskCreator)
        {
            _taskCreator = taskCreator;
        }

        public virtual void Execute(Directive directive, IPowerShellConnection powerShellConnection, AdmiralContext context)
        {
            foreach (var taskDescription in directive.TaskDescriptions)
            {
                try
                {
                    Logger.Log.Info($"Executing {taskDescription.TaskName} in {taskDescription.Module}");
                    var task = _taskCreator.CreateTask(taskDescription, context);
                    var result = task.Execute(powerShellConnection);
                    Console.WriteLine(result.Output);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error("Could not execute task", ex);
                    break;
                }
            }
        }
    }
}