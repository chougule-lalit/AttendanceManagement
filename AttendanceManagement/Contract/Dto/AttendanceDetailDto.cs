using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract.Dto
{
    public class AttendanceDetailDto
    {
        public int UserId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan TimeIn { get; set; }

        public TimeSpan TimeOut { get; set; }

        public int AttendanceTypeId { get; set; }

        public string Description { get; set; }
    }
}
