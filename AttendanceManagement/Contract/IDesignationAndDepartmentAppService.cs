using AttendanceManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract
{
    public interface IDesignationAndDepartmentAppService
    {
        Task CreateOrUpdateDesignation(DesignationDto input);
        Task<DesignationDto> GetDesignationAsync(int id);
        Task DeleteDesignationAsync(int id);
        Task CreateOrUpdateDepartment(DepartmentDto input);
        Task<DepartmentDto> GetDepartmentAsync(int id);
        Task DeleteDepartmentAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentDropdownAsync();
        Task<List<DesignationDto>> GetDesignationDropdownAsync();

    }
}
