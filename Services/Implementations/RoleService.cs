using redFlag.Dtos;
using redFlag.Entities;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse<RoleDto>> Create(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get(a => a.Name == model.Name);
            if (roleExist != null) return new BaseResponse<RoleDto>
            {
                Message = "Role already exist",
                Status = false,

            };

            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
            };

            await _roleRepository.Create(role);

            return new BaseResponse<RoleDto>
            {
                Message = "Created Successfully",
                Status = true,
                Data = new RoleDto
                {
                    Name = role.Name
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> Get(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "Role not found",
                Status = false,
            };
            return new BaseResponse<RoleDto>
            {
                Message = "Successful",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    Users = role.UserRoles.Select(v => new UserDto
                    {
                        FirstName = v.User.FirstName,
                        LastName = v.User.LastName,
                        Email = v.User.Email,

                    }).ToList(),
                },
            };
        }

        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            var listOfRoles = roles.ToList().Select(a => new RoleDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Users = a.UserRoles.Select(b => new UserDto
                {
                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName,
                    Email = b.User.Email,
                }).ToList(),
            });
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfRoles,
            };

        }

        public async Task<BaseResponse<RoleDto>> Update(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "Role Not Found",
                Status = false,
            };

            role.Name = model.Name;
            role.Description = model.Description;

            await _roleRepository.Update(role);

            return new BaseResponse<RoleDto>
            {
                Message = "Successfully Updated",
                Status = true,
                Data = new RoleDto
                {
                    Name = model.Name
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> Delete(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null) return new BaseResponse<RoleDto>
            {
                Message = "Role Not Found",
                Status = false,
            };

            role.IsDeleted = true;
            _roleRepository.Save();

            return new BaseResponse<RoleDto>
            {
                Message = "Delete Successful",
                Status = true,
            };
        }
    }
}
