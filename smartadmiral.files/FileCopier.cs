using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using smartadmiral.common;

namespace smartadmiral.files
{
    public class FileCopier
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool LogonUser(string username, string domain,
            IntPtr password, int logonType,
            int logonProvider, ref IntPtr token);

        public static bool Copy(string src, string dest, AdmiralContext context)
        {
            IntPtr adminToken = default(IntPtr);
            WindowsImpersonationContext wic = null;
            var passwordPtr = Marshal.SecureStringToGlobalAllocUnicode(context.Password);

            try
            {

                if (LogonUser(context.UserName, context.Hostname, passwordPtr, 9, 0, ref adminToken))
                {
                    var widAdmin = new WindowsIdentity(adminToken);
                    wic = widAdmin.Impersonate();
                    var host = $@"\\{context.Hostname}";
                    dest = dest.Replace(':', '$');
                    dest = Path.Combine(host, dest);
                    File.Copy(src, dest);
                    return true;
                }
                return false;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(passwordPtr);

                wic?.Undo();
            }
        }
    }
}