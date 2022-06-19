using AttendanceManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract
{
    public interface IAttendanceAppService
    {
        Task CreateOrUpdate(AttendanceDetailDto input);
        Task<AttendanceDetailDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<AttendanceDetailDto>> FetchAttendanceDetailListAsync(GetAttendanceDetailInputDto input);
        List<PastMonthDropdownDto> GetPastMonthDropdownAsync();
        Task<List<AttendanceDetailDto>> FetchAttendanceReportAsync(AttendanceReportInputDto input);
    }
}
