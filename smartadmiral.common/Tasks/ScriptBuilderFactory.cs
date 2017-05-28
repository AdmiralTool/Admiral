using System;
using System.Management.Automation.Runspaces;

namespace smartadmiral.common.Tasks
{
    public interface IPowerShellConnection : IDisposable
    {
        IPowerShellScriptBuilder CreateScriptBuilder();
    }


    public class PowerShellConnection : IPowerShellConnection
    {
        private readonly Runspace _runspace;

        public PowerShellConnection(Runspace runspace)
        {
            _runspace = runspace;
        }

        public IPowerShellScriptBuilder CreateScriptBuilder()
        {
            return new PowerShellScriptExecutor(_runspace);
        }

        public void Dispose()
        {
            _runspace.Dispose();
        }
    }
}