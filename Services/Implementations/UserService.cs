using redFlag.Dtos;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<UserDto>> AsignInitiatorRole(int id, string name)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "user not found",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Initiator");

            user.UserRoles.Add(new Entities.UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = "successfull",

            };

        }

        public async Task<BaseResponse<UserDto>> AsignRoles(int id, List<int> roleIds)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "user not found",
                Status = false,
            };
            return new BaseResponse<UserDto>
            {

            };

            /*var roles = await _roleRepository.GetSelected(roleIds);
            foreach(var role in roles)
            {
                
            };*/



        }

        public async Task<BaseResponse<UserDto>> Get(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "User not found",
                Status = false,
            };
            return new BaseResponse<UserDto>
            {
                Message = "Success",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Image = user.Image,
                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Role.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAll();
            var listOfUsers = users.ToList().Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Image = user.Image,
                Roles = user.UserRoles.Select(a => new RoleDto
                {
                    Id = a.Role.Id,
                    Name = a.Role.Name,
                    Description = a.Role.Description,
                }).ToList(),
            });

            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "success",
                Status = true,
                Data = listOfUsers,
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.Get(a => a.Email == model.Email);
            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "email or password incorrect",
                Status = false,
            };
            return new BaseResponse<UserDto>
            {
                Message = "login successful",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Image = user.Image,
                    AgencyAbbreviation = user?.Staff?.Agency?.Abbreviation,
                    AgencyName = user?.Staff?.Agency?.Name,
                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Role.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                },
            };
        }
    }
}
