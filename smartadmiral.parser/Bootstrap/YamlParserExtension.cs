using Microsoft.Practices.Unity;
using smartadmiral.common.Parsers;

namespace smartadmiral.parser.Bootstrap
{
    public class YamlParserExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IParser, YamlParser>();
        }
    }
}
