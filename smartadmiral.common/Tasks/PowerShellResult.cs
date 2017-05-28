using System.Collections.Generic;
using System.Management.Automation;

namespace smartadmiral.common.Tasks
{
    public class PowerShellResult
    {
        public PowerShellResult(bool success, IEnumerable<PSObject> result)
        {
            Success = success;
            Result = result;
        }

        public PowerShellResult(bool success, IEnumerable<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public IEnumerable<PSObject> Result { get; }
        public bool Success { get; }
        public IEnumerable<string> Errors { get; }
    }
}