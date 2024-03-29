﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract.Dto
{
    public class AttendanceDetailDto
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public DateTime? LeaveFromDate { get; set; }

        public DateTime? LeaveToDate { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public int AttendanceTypeId { get; set; }

        public string Description { get; set; }
        public string AttendDate { get; set; }
    }

    public class GetAttendanceDetailInputDto : PagedResultInput
    {

    }

    public class AttendanceReportInputDto
    {
        public PastMonthDropdownDto PastMonth { get; set; }

        public int UserId { get; set; }
    }
}
