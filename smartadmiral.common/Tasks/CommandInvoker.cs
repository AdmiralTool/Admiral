using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace smartadmiral.common.Tasks
{
    public interface IPowerShellScriptBuilder
    {
        IPowerShellScriptExecutor AddScript(string script);
    }

    public interface IPowerShellScriptExecutor
    {
        IPowerShellScriptExecutor AddScript(string script);
        PowerShellResult Invoke();
    }

    public class PowerShellScriptExecutor : IPowerShellScriptBuilder, IPowerShellScriptExecutor
    {
        private readonly List<string> _scripts = new List<string>();
        private readonly Runspace _runspace;

        public PowerShellScriptExecutor(Runspace runspace)
        {
            _runspace = runspace;
        }

        public IPowerShellScriptExecutor AddScript(string script)
        {
            _scripts.Add(script);
            return this;
        }

        public PowerShellResult Invoke()
        {
            var powerShell = CreatePowerShell();
            AddScripts(powerShell);

            var result = powerShell.Invoke();
            if (PowerShellHasErrors(powerShell))
            {
                return new PowerShellResult(false, powerShell.Streams.Error.Select(x => x.ToString()));
            }
            powerShell.Dispose();
            return new PowerShellResult(true, result);
        }

        private void AddScripts(PowerShell powerShell)
        {
            _scripts.ForEach(s => powerShell.AddScript(s));
        }

        private PowerShell CreatePowerShell()
        {
            var powerShell = PowerShell.Create();
            powerShell.Runspace = _runspace;
            return powerShell;
        }

        private static bool PowerShellHasErrors(PowerShell powerShell)
        {
            return powerShell.Streams.Error.Count > 0;
        }
    }
}