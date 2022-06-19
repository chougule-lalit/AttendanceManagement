using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Entities
{
    public class AttendanceType : BaseEntity
    {
        public virtual string Type { get; set; }

        public virtual string Description { get; set; }
    }
}
