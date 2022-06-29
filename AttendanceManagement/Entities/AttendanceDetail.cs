using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Entities
{
    public class AttendanceDetail : BaseEntity
    {
        public virtual int UserId { get; set; }

        public virtual DateTime AttendanceDate { get; set; }

        public  virtual DateTime? LeaveFromDate { get; set; }

        public  virtual DateTime? LeaveToDate { get; set; }

        public virtual DateTime? TimeIn { get; set; }

        public virtual DateTime? TimeOut { get; set; }

        public virtual AttendanceType AttendanceType { get; set; }

        public virtual int AttendanceTypeId { get; set; }

        public virtual string Description { get; set; }
    }
}
