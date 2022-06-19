using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Entities
{
    public class UserMaster : BaseEntity
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public virtual RoleMaster Role { get; set; }
        
        public virtual int RoleId { get; set; }

        public virtual string Password { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual int DesignationId { get; set; }

        public virtual Department Department { get; set; }

        public virtual int DepartmentId { get; set; }
    }
}
