using AttendanceManagement.Contract;
using AttendanceManagement.Contract.Dto;
using AttendanceManagement.Data;
using AttendanceManagement.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceManagement.Application
{
    public class AttendanceAppService : IAttendanceAppService
    {
        private readonly AttendanceManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public AttendanceAppService(
            AttendanceManagementDbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdate(AttendanceDetailDto input)
        {
            if (input.Id.HasValue)
            {
                var data = await _dbContext.AttendanceDetails.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (data != null)
                {
                    _mapper.Map(input, data);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<AttendanceDetail>(input);
                userToCreate.AttendanceDate = DateTime.Now;
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AttendanceDetailDto> GetAsync(int id)
        {
            var data = await _dbContext.AttendanceDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<AttendanceDetailDto>(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.AttendanceDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.AttendanceDetails.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<AttendanceDetailDto>> FetchAttendanceDetailListAsync(GetAttendanceDetailInputDto input)
        {
            var data = await _dbContext.AttendanceDetails.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<AttendanceDetailDto>
            {
                Items = _mapper.Map<List<AttendanceDetailDto>>(returnData),
                TotalCount = count
            };
        }

        public List<PastMonthDropdownDto> GetPastMonthDropdownAsync()
        {
            return (from r in Enumerable.Range(0, 11)
                    select new PastMonthDropdownDto
                    {
                        Month = DateTime.Now.AddMonths(0 - r).Month,
                        Year = DateTime.Now.AddMonths(0 - r).Year,
                        Lable = $"{(CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(0 - r).Month)).Substring(0, 3)} {DateTime.Now.AddMonths(0 - r).Year}",
                    }).ToList();
        }

        public async Task<List<AttendanceDetailDto>> FetchAttendanceReportAsync(AttendanceReportInputDto input)
        {
            var output = new List<AttendanceDetailDto>();
            var data = await _dbContext.AttendanceDetails.Where(x => x.UserId == input.UserId
                                && x.AttendanceDate.Date.Month == input.PastMonth.Month
                                && x.AttendanceDate.Date.Year == input.PastMonth.Year)
                        .ToListAsync();

            var groupedData = data.GroupBy(x => x.AttendanceTypeId).ToList();
            foreach (var item in groupedData)
            {
                var list = _mapper.Map<List<AttendanceDetailDto>>(item.ToList());
                if (item.Key == Convert.ToInt32(AttendanceTypeEnum.Present))
                    output.AddRange(list);

                var types = new List<int>
                {
                    Convert.ToInt32(AttendanceTypeEnum.CasualLeave),
                    Convert.ToInt32(AttendanceTypeEnum.CompOff),
                    Convert.ToInt32(AttendanceTypeEnum.PersonalLeave),
                    Convert.ToInt32(AttendanceTypeEnum.SickLeave)
                };

                if (types.Contains(item.Key))
                {
                    foreach (var leaveItem in list)
                    {
                        var totalDays = (int)(leaveItem.LeaveToDate - leaveItem.LeaveFromDate).Value.TotalDays;
                        if (totalDays > 1)
                        {
                            output.AddRange(GetAllDaysList(leaveItem));
                        }
                        else
                        {
                            output.Add(leaveItem);
                        }
                    }
                }
            }

            return output;

        }

        public List<AttendanceDetailDto> GetAllDaysList(AttendanceDetailDto input)
        {
            var output = new List<AttendanceDetailDto>();
            for (var date = input.LeaveFromDate.Value.Date; date.Date <= input.LeaveToDate.Value.Date; date = date.AddDays(1))
            {
                Console.WriteLine(date);
                input.AttendanceDate = date;
                var data = new AttendanceDetailDto();
                data = _mapper.Map(input, data);
                output.Add(data);
            }
            return output;
        }
    }
}
