using System;
using System.Security;
using smartadmiral.common;
using smartadmiral.common.Logger;
using smartadmiral.common.Parsers;

namespace smartadmiral.console.Core
{
    internal class Admiral
    {
        private readonly IParser _parser;
        private readonly IConnector _connector;
        private readonly TasksExecutor _tasksExecutor;

        public Admiral(IParser parser, IConnector connector, TasksExecutor tasksExecutor)
        {
            _parser = parser;
            _connector = connector;
            _tasksExecutor = tasksExecutor;
        }

        public void Run(string shipsFileName, string directiveFileName)
        {
            var directive = _parser.Parse(shipsFileName, directiveFileName);

            var username = directive.Credentials.Username;
            var password = directive.Credentials.Password;
            foreach (var machine in directive.Hostnames)
            {
                Logger.Log.Info($"Connecting to {machine}...");
                var securePassword = CreateSecurePassword(password);

                using (var powerShellConnection = _connector.Connect(machine, username, securePassword))
                {
                    var context = new AdmiralContext
                    {
                        Hostname = machine,
                        Password = securePassword,
                        UserName = username,
                    };

                    Logger.Log.Info($"Successfully connected to {machine}.");

                    _tasksExecutor.Execute(directive, powerShellConnection, context);
                }
            }
        }

        private SecureString CreateSecurePassword(string password)
        {
            var securePassword = new SecureString();
            foreach (var ch in password)
            {
                securePassword.AppendChar(ch);
            }
            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}