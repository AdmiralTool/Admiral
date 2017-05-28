using System;
using Microsoft.Practices.Unity;
using smartadmiral.common.Logger;
using smartadmiral.console.Args;
using smartadmiral.console.Bootstrap;
using smartadmiral.console.Core;

namespace smartadmiral.console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Logger.InitLogger();
            using (var container = new UnityContainer())
            {
                container.AddNewExtension<Bootstrapper>();
                Logger.Log.Info("Bootstrapping is done");

                try
                {
                    var parsedArgs = ArgsParser.ParseArgs(args);
                    var admiral = container.Resolve<Admiral>();
                    admiral.Run(parsedArgs.PathToShips, parsedArgs.PathToDirective);
                }
                catch (ArgsParserException ex)
                {
                    Logger.Log.Error("Could not parse input arguments, exception is ", ex);
                    throw;
                }

                Console.ReadKey();
            }
        }
    }
}
