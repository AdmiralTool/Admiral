using System.Collections.Generic;

namespace smartadmiral.common.Directives
{
    public class Directive
    {
        public Directive(IEnumerable<string> hostnames, IEnumerable<TaskDescription> taskDescriptions, Credentials credentials)
        {
            Hostnames = hostnames;
            TaskDescriptions = taskDescriptions;
            Credentials = credentials;
        }

        public IEnumerable<string> Hostnames { get; }
        public IEnumerable<TaskDescription> TaskDescriptions { get; }
        public Credentials Credentials { get; }
    }
}