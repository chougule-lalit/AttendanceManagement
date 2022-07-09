using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract.Dto
{
    public class DesignationDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }

    public class DepartmentDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
