using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }
        public int RoleID { get; set; } // ==> FK
        public Role Role { get; set; } // ==> Navigation Property
        public Doctor? Doctor { get; set; } // ==> Navigation Property
        public Patient? Patient { get; set; } // ==> Navigation Property

    }
}
