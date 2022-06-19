using AttendanceManagement.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Application
{
    public class AttendanceAppService : IAttendanceAppService
    {
        private readonly IAttendanceAppService _attendanceAppService;
        private readonly IMapper _mapper;

        public AttendanceAppService(
            IAttendanceAppService attendanceAppService,
            IMapper mapper
            )
        {
            _attendanceAppService = attendanceAppService;
            _mapper = mapper;
        }
    }
}
