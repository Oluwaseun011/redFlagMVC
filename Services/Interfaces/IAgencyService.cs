using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IAgencyService
    {
        Task<BaseResponse<AgencyDto>> Create(CreateAgencyRequestModel model);
        Task<BaseResponse<AgencyDto>> Update(int id, UpdateAgencyRequestModel model);
        Task<BaseResponse<AgencyDto>> Get(int id);
        Task<BaseResponse<IEnumerable<AgencyDto>>> GetAll();
        Task<BaseResponse<AgencyDto>> Delete(int id);
    }
}
