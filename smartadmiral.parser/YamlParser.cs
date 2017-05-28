using System;
using System.Collections.Generic;
using smartadmiral.common.Directives;
using smartadmiral.common.Logger;
using smartadmiral.parser.DTOs.Directives;
using IParser = smartadmiral.common.Parsers.IParser;

namespace smartadmiral.parser
{
    public class YamlParser : IParser
    {
        private readonly ShipsParser _shipsParser;
        private readonly DirectiveParser _directiveParser;

        public YamlParser()
        {
            _shipsParser = new ShipsParser();
            _directiveParser = new DirectiveParser();
        }

        public Directive Parse(string shipsFileName, string directiveFileName)
        {
            var directive = _directiveParser.ParseDirective(directiveFileName);

            var hostNames = _shipsParser.GetHostnames(shipsFileName, directive.Ships);

            var tasks = CreateTasks(directive.Tasks);
            var credentials = CreateCredentials(directive.Username, directive.Password);

            return new Directive(hostNames, tasks, credentials);
        }

        private Credentials CreateCredentials(string username, string password)
        {
            return new Credentials(username, password);
        }

        private List<TaskDescription> CreateTasks(IEnumerable<TaskDto> tasksDtos)
        {
            var tasks = new List<TaskDescription>();
            foreach (var directiveTask in tasksDtos)
            {
                var desiredTask = GetTaskFromContainer(directiveTask);
                if (desiredTask != null)
                    tasks.Add(desiredTask);
            }
            return tasks;
        }

        private TaskDescription GetTaskFromContainer(TaskDto directiveTask)
        {
            try
            {
                var moduleTaskNamesPair = directiveTask.Module.Split('.');
                var moduleName = moduleTaskNamesPair[0];
                var taskName = moduleTaskNamesPair[1];

                return new TaskDescription(moduleName, taskName, directiveTask.Args);
            }
            catch (IndexOutOfRangeException)
            {
                Logger.Log.Error($"Could not find task: name {directiveTask.Name} with args {directiveTask.Args} in module {directiveTask.Module}");
                return null;
            }
        }
    }
}