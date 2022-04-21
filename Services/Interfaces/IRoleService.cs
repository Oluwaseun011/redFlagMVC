using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> Update(int id, UpdateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> Get(int id);
        Task<BaseResponse<IEnumerable<RoleDto>>> GetAll();
        Task<BaseResponse<RoleDto>> Delete(int id);
    }
}
