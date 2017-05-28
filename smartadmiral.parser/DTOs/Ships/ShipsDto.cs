using System.Collections.Generic;

namespace smartadmiral.parser.DTOs.Ships
{
    public class ShipsDto
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public IEnumerable<string> Hosts { get; set; }
    }
}