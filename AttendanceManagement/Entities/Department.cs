using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Entities
{
    public class Department:BaseEntity
    {
        public virtual string Name { get; set; }
    }
}
