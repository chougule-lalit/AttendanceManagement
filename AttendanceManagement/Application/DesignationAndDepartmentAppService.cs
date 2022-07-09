using AttendanceManagement.Contract;
using AttendanceManagement.Contract.Dto;
using AttendanceManagement.Data;
using AttendanceManagement.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Application
{
    public class DesignationAndDepartmentAppService: IDesignationAndDepartmentAppService
    {
        private readonly AttendanceManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public DesignationAndDepartmentAppService(
            AttendanceManagementDbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateDesignation(DesignationDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.Designations.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<Designation>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<DesignationDto> GetDesignationAsync(int id)
        {
            var data = await _dbContext.Designations.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<DesignationDto>(data);
        }

        public async Task DeleteDesignationAsync(int id)
        {
            var data = await _dbContext.Designations.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.Designations.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateOrUpdateDepartment(DepartmentDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<Department>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<DepartmentDto> GetDepartmentAsync(int id)
        {
            var data = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<DepartmentDto>(data);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var data = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.Departments.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<DepartmentDto>> GetDepartmentDropdownAsync()
        {
            var data = await _dbContext.Departments.ToListAsync();
            return _mapper.Map< List<DepartmentDto>>(data);
        }

        public async Task<List<DesignationDto>> GetDesignationDropdownAsync()
        {
            var data = await _dbContext.Designations.ToListAsync();
            return _mapper.Map<List<DesignationDto>>(data);
        }

        public async Task<PagedResultDto<DesignationDto>> FetchDesignationListAsync(GetDesignationInputDto input)
        {
            var data = await _dbContext.Designations.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<DesignationDto>
            {
                Items = _mapper.Map<List<DesignationDto>>(returnData),
                TotalCount = count
            };
        }

        public async Task<PagedResultDto<DepartmentDto>> FetchDepartmentListAsync(GetDesignationInputDto input)
        {
            var data = await _dbContext.Departments.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<DepartmentDto>
            {
                Items = _mapper.Map<List<DepartmentDto>>(returnData),
                TotalCount = count
            };
        }
    }
}
