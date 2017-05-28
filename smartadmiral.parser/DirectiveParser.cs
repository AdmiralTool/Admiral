using System.IO;
using smartadmiral.parser.DTOs.Directives;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace smartadmiral.parser
{
    internal class DirectiveParser
    {
        public DirectiveDto ParseDirective(string filename)
        {
            var input = File.ReadAllText(filename);
            var deserializer = CreateDeserializer();
            var directive = deserializer.Deserialize<DirectiveDto>(input);
            return directive;
        }

        private static Deserializer CreateDeserializer()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            return deserializer;
        }
    }
}