using System.Security;

namespace smartadmiral.common
{
    public class AdmiralContext
    {
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string Hostname { get; set; }
    }
}