using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IStaffService
    {
        Task<BaseResponse<StaffDto>> Create(CreateStaffRequestModel model);
        Task<BaseResponse<StaffDto>> CreateAgencyAdmin(CreateAgencyAdminRequestModel model, int agencyId);
        Task<BaseResponse<StaffDto>> Update(int id, UpdateStaffRequestModel model);
        Task<BaseResponse<StaffDto>> Get(int id);
        Task<BaseResponse<IEnumerable<StaffDto>>> GetAll();
    }
}
