using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veritas.BusinessLayer.Providers
{
    public class VeritasMembershipUser
    {
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastLockoutDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public bool IsOnline { get; set; }
        public bool IsLockedOut { get; set; }
        public string About { get; set; }
        public bool IsAdmin { get; set; }
    }
}
