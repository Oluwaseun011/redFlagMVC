using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IBranchService
    {
        Task<BaseResponse<BranchDto>> Create(CreateBranchRequestModel model, int agencyId);
        Task<BaseResponse<BranchDto>> CreateHeadquaters(CreateHeadquatersRequestModel model, int agencyId);
        Task<BaseResponse<BranchDto>> Update(int id, UpdateBranchRequestModel model);
        Task<BaseResponse<BranchDto>> Get(int id);
        Task<BaseResponse<IEnumerable<BranchDto>>> GetAll();
        Task<BaseResponse<BranchDto>> Delete(int id);
    }
}
