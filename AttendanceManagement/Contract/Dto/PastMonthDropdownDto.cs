using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract.Dto
{
    public class PastMonthDropdownDto
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string Lable { get; set; }
    }
}
