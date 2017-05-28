using System.Collections.Generic;

namespace smartadmiral.parser.DTOs.Directives
{
    internal class DirectiveDto
    {
        public string Ships { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}