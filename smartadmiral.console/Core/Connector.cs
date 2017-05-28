using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using smartadmiral.common.Logger;
using smartadmiral.common.Tasks;

namespace smartadmiral.console.Core
{
    public interface IConnector
    {
        IPowerShellConnection Connect(string hostname, string username, SecureString password);
    }

    public class Connector : IConnector
    {
        private Runspace _runspace;

        public IPowerShellConnection Connect(string hostname, string username, SecureString password)
        {
            try
            {
                string shell = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";
                var targetWsMan = new Uri($"http://{hostname}:5985/wsman");

                var cred = new PSCredential(username, password);
                var connectionInfo = CreateConnectionInfo(targetWsMan, shell, cred);
                _runspace = RunspaceFactory.CreateRunspace(connectionInfo);
                _runspace.Open();
                Console.WriteLine("Connected to {0}", targetWsMan);
                Console.WriteLine("As {0}", username);

                return new PowerShellConnection(_runspace);
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Could not connect to machine {hostname}", ex);
                throw;
            }
        }

        private static WSManConnectionInfo CreateConnectionInfo(Uri targetWsMan, string shell, PSCredential cred)
        {
            var connectionInfo = new WSManConnectionInfo(targetWsMan, shell, cred)
            {
                OperationTimeout = 4*60*1000,
                OpenTimeout = 1*60*1000
            };
            return connectionInfo;
        }
    }
}