using AttendanceManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract
{
    public interface IEnquiryAppService
    {
        Task CreateOrUpdateUser(EnquiryDto input);
        Task<EnquiryDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<EnquiryDto>> FetchEnquiryListAsync(GetEnquiryInputDto input);
    }
}
