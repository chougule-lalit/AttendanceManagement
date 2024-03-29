﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AttendanceManagement.Contract;
using AttendanceManagement.Contract.Dto;
using AttendanceManagement.Data;
using AttendanceManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Application
{
    public class UserMasterAppService : IUserMasterAppService
    {
        private readonly AttendanceManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserMasterAppService(
            AttendanceManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateUser(UserMasterCreateUpdateDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.UserMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<UserMaster>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<UserMasterDto>> FetchUserListAsync(GetUserInput input)
        {
            var data = from u in _dbContext.UserMasters
                       join r in _dbContext.RoleMasters on u.RoleId equals r.Id
                       join d in _dbContext.Designations on u.DesignationId equals d.Id
                       join dp in _dbContext.Departments on u.DepartmentId equals dp.Id
                       select new UserMasterDto
                       {
                           Email = u.Email,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Id = u.Id,
                           Phone = u.Phone,
                           RoleId = r.Id,
                           RoleName = r.Name,
                           DepartmentId = u.DepartmentId,
                           DesignationId = u.DesignationId,
                           DepartmentName = dp.Name,
                           DesignationName = d.Name
                       };

            if (!string.IsNullOrEmpty(input.Search))
                data = data.Where(x => x.FirstName.ToLower().Contains(input.Search.ToLower()) ||
                            x.LastName.ToLower().Contains(input.Search.ToLower()));

            var count = data.Count();

            var userList = await data.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

            return new PagedResultDto<UserMasterDto>
            {
                Items = userList,
                TotalCount = count
            };
        }

        public async Task<UserMasterDto> GetUserAsync(int id)
        {
            var user = await _dbContext.UserMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return _mapper.Map<UserMasterDto>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.UserMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                _dbContext.UserMasters.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<LoginOutputDto> LoginAsync(LoginInputDto input)
        {
            var output = new LoginOutputDto();
            var user = await _dbContext.UserMasters.FirstOrDefaultAsync(x => x.Email == input.Email);
            if (user != null)
            {
                if (input.Password == user.Password)
                {
                    output.IsSuccess = true;
                    output.Role = (RoleEnum)user.RoleId;
                    output.Id = user.Id;
                }
                else
                    return output;
            }

            return output;
        }

        public async Task<List<UserDropdownDto>> GetUserListPerRoleDropDownAsync(int roleId)
        {
            return await _dbContext.UserMasters.Where(x => x.RoleId == roleId)
                .Select(x => new UserDropdownDto
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}"
                })
                .ToListAsync();
        }

        public async Task<List<UserDropdownDto>> GetAllUserListDropDownAsync()
        {
            return await _dbContext.UserMasters
                .Select(x => new UserDropdownDto
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}"
                })
                .ToListAsync();
        }
    }
}
