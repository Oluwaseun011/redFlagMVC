using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IInitiatorService
    {
        Task<BaseResponse<InitiatorDto>> MiniCreate(MiniCreateInitiatorRequestModel model);
        Task<BaseResponse<InitiatorDto>> Create(CreateInitiatorRequestModel model);
        Task<BaseResponse<InitiatorDto>> Update(int id, UpdateInitiatorRequestModel model);
        Task<BaseResponse<InitiatorDto>> Get(int id);
        Task<BaseResponse<IEnumerable<InitiatorDto>>> GetAll();
    }
}
