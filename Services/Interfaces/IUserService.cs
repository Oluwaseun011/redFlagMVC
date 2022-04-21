using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model);
        Task<BaseResponse<UserDto>> Get(int id);
        Task<BaseResponse<IEnumerable<UserDto>>> GetAll();
        Task<BaseResponse<UserDto>> AsignRoles(int id, List<int> roleIds);
        Task<BaseResponse<UserDto>> AsignInitiatorRole(int id, string name);
    }
}
