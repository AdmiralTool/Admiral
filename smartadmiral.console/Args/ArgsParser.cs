using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using smartadmiral.common.Logger;

namespace smartadmiral.console.Args
{
    public class ArgsParserException : Exception
    {
        public ArgsParserException(string message) : base(message)
        {
        }
    }

    internal class ArgsParser
    {
        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
            out int pNumArgs);

        public static AdmiralArgs ParseArgs(string[] rawArgs)
        {
            var joinedArgs = string.Join(" ", rawArgs);
            Logger.Log.Info($"Parsing args [{joinedArgs}]");

            var parsedArgs = CommandLineToArgs(joinedArgs).ToList();

            var pathToShips = GetPathToShips(parsedArgs);

            var pathToDirective = GetPathToDirective(parsedArgs);

            return new AdmiralArgs(pathToShips, pathToDirective);
        }

        private static string GetPathToDirective(IList<string> parsedArgs)
        {
            var indexOfDirectiveArgument = GetIndexOfParameter("-directive", parsedArgs);
            return GetValueByIndex(indexOfDirectiveArgument, parsedArgs);
        }

        private static string GetPathToShips(IList<string> parsedArgs)
        {
            var indexOfShipsArgument = GetIndexOfParameter("-ships", parsedArgs);
            return GetValueByIndex(indexOfShipsArgument, parsedArgs);
        }

        private static int GetIndexOfParameter(string param, IList<string> parsedArgs)
        {
            var index = parsedArgs.IndexOf(param);
            if (index == -1)
                throw new ArgsParserException($"Could not find parameter {param}");

            return index;
        }

        private static string GetValueByIndex(int index, IList<string> parsedArgs)
        {
            try
            {
                return parsedArgs.ElementAt(index + 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgsParserException("Could not parse input string");
            }
        }

        private static string[] CommandLineToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i*IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}