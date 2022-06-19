using AttendanceManagement.Contract;
using AttendanceManagement.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : IAttendanceAppService
    {
        private readonly IAttendanceAppService _attendanceAppService;

        public AttendanceController(IAttendanceAppService attendanceAppService)
        {
            _attendanceAppService = attendanceAppService;
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task CreateOrUpdate(AttendanceDetailDto input)
        {
            return _attendanceAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _attendanceAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("fetchAttendanceDetailList")]
        public virtual Task<PagedResultDto<AttendanceDetailDto>> FetchAttendanceDetailListAsync(GetAttendanceDetailInputDto input)
        {
            return _attendanceAppService.FetchAttendanceDetailListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<AttendanceDetailDto> GetAsync(int id)
        {
            return _attendanceAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("getPastMonthDropdown")]
        public virtual List<PastMonthDropdownDto> GetPastMonthDropdownAsync()
        {
            return _attendanceAppService.GetPastMonthDropdownAsync();
        }

        [HttpPost]
        [Route("fetchAttendanceReport")]
        public virtual Task<List<AttendanceDetailDto>> FetchAttendanceReportAsync(AttendanceReportInputDto input)
        {
            return _attendanceAppService.FetchAttendanceReportAsync(input);
        }


    }
}
