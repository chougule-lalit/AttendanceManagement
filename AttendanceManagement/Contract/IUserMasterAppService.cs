﻿using AttendanceManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Contract
{
    public interface IUserMasterAppService
    {
        Task CreateOrUpdateUser(UserMasterCreateUpdateDto input);

        Task<PagedResultDto<UserMasterDto>> FetchUserListAsync(GetUserInput input);

        Task<UserMasterDto> GetUserAsync(int id);

        Task DeleteUserAsync(int id);

    }
}
