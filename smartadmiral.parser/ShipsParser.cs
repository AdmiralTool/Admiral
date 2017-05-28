using System.Collections.Generic;
using System.IO;
using System.Linq;
using smartadmiral.parser.DTOs.Ships;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace smartadmiral.parser
{
    internal class ShipsParser
    {
        public IEnumerable<string> GetHostnames(string fileName, string shipsGroupName)
        {
            var hostNames = new List<string>();
            var ships = DeserializeShips(fileName);

            var currentShipsGroup = ships.First(x => x.Name == shipsGroupName);
            hostNames.AddRange(currentShipsGroup.Hosts);

            while (currentShipsGroup.Parent != null)
            {
                var currentShipsGroupName = currentShipsGroup.Parent;
                currentShipsGroup = ships.First(x => x.Name == currentShipsGroupName);
                hostNames.AddRange(currentShipsGroup.Hosts);
            }

            return hostNames;
        }

        private List<ShipsDto> DeserializeShips(string fileName)
        {
            var result = new List<ShipsDto>();
            var inputText = File.ReadAllText(fileName);
            var input = new StringReader(inputText);
            var deserializer = CreateDeserializer();

            var parser = new Parser(input);

            parser.Expect<StreamStart>();

            while (parser.Accept<DocumentStart>())
            {
                var shipsDtos = deserializer.Deserialize<List<ShipsDto>>(parser);
                result.Add(shipsDtos.First());
            }

            return result;
        }

        private Deserializer CreateDeserializer()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .Build();
            return deserializer;
        }
    }
}