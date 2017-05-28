using log4net;
using log4net.Config;

namespace smartadmiral.common.Logger
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("logger");

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}