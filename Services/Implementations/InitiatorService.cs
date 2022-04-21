using Microsoft.AspNetCore.Hosting;
using redFlag.Dtos;
using redFlag.Entities;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Services.Implementations
{
    public class InitiatorService : IInitiatorService
    {
        private readonly IInitiatorRepository _initiatorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webroot;

        public InitiatorService(IInitiatorRepository initiatorRepository, IUserRepository userRepository, IRoleRepository roleRepository, IWebHostEnvironment webroot)
        {
            _initiatorRepository = initiatorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _webroot = webroot;
        }

        public async Task<BaseResponse<InitiatorDto>> Create(CreateInitiatorRequestModel model)
        {
            var initiatorExist = await _initiatorRepository.Get(b =>b.User.Email == model.Email);
            if (initiatorExist != null)
            {
                return new BaseResponse<InitiatorDto>
                {
                    Message = "user already exist",
                    Status = false,
                };
            }

            var userImage = "";
            if (model.Image != null)
            {
                string imagePath = Path.Combine(_webroot.WebRootPath, "UserImage");
                Directory.CreateDirectory(imagePath);
                string userImageType = model.Image.ContentType.Split('/')[1];
                userImage = $"{Guid.NewGuid()}.{userImageType}";
                var rightpath = Path.Combine(imagePath, userImage);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.Image.CopyTo(filestream);
                }
            }

            var role = await _roleRepository.Get(b => b.Name == "Initiator");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                IsActive = true,
                PhoneNumber = model.PhoneNumber,
                Image = userImage,
                UserRoles = new List<UserRole>()
                
                                
            };

            

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user,
            };

            user.UserRoles.Add(userRole);

            var initiator = new Initiator
            {
                UserId = user.Id,
                User = user,
            };

            await _userRepository.Create(user);
            await _initiatorRepository.Create(initiator);


            return new BaseResponse<InitiatorDto>
            {
                Message = "Created successfully",
                Status = true,
                Data = new InitiatorDto
                {
                    FirstName = initiator.User.FirstName,
                    LastName = initiator.User.LastName,
                    Email = initiator.User.Email,

                }
            };
        }

        public async Task<BaseResponse<InitiatorDto>> MiniCreate(MiniCreateInitiatorRequestModel model)
        {
            var initiatorExist = await _initiatorRepository.Get(a => a.User.Email == model.Email);
            if (initiatorExist != null) return new BaseResponse<InitiatorDto>
            {
                Message = "user already exist",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Initiator");

            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Address = model.Password,
                IsActive = true,
                PhoneNumber = model.PhoneNumber,
            };

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user,
            };

            user.UserRoles.Add(userRole);
            var initiator = new Initiator
            {
                UserId = user.Id,
                User = user,
            };

            await _userRepository.Create(user);
            await _initiatorRepository.Create(initiator);

            return new BaseResponse<InitiatorDto>
            {
                Message = "Created successfully, complete registration within 30days",
                Status = true,
                Data = new InitiatorDto
                { 
                    Email = initiator.User.Email,
                }
            };
        }

        public async Task<BaseResponse<InitiatorDto>> Get(int id)
        {
            var initiator = await _initiatorRepository.Get(id);
            if (initiator == null) return new BaseResponse<InitiatorDto>
            {
                Message = "User not found",
                Status = false,
            };

            return new BaseResponse<InitiatorDto>
            {
                Message = "success",
                Status = true,
                Data = new InitiatorDto
                {
                    Id = initiator.Id,
                    UserId = initiator.User.Id,
                    FirstName = initiator.User.FirstName,
                    LastName = initiator.User.LastName,
                    Email = initiator.User.Email,
                    Address = initiator.User.Address,
                    PhoneNumber = initiator.User.PhoneNumber,
                    Image = initiator.User.Image,
                    Roles = initiator.User.UserRoles.Select(b => new RoleDto
                    {
                        Id = b.RoleId,
                        Name = b.Role.Name,
                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<InitiatorDto>>> GetAll()
        {
            var initiators = await _initiatorRepository.GetAll();
            var listOfInitiators = initiators.ToList().Select(initiator => new InitiatorDto
            {
                Id = initiator.Id,
                UserId = initiator.User.Id,
                FirstName = initiator.User.FirstName,
                LastName = initiator.User.LastName,
                Email = initiator.User.Email,
                Address = initiator.User.Address,
                PhoneNumber = initiator.User.PhoneNumber,
                Image = initiator.User.Image,
                Roles = initiator.User.UserRoles.Select(b => new RoleDto
                {
                    Id = b.RoleId,
                    Name = b.Role.Name,
                }).ToList(),
            });

            return new BaseResponse<IEnumerable<InitiatorDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfInitiators,
            };
        }

        

        public async Task<BaseResponse<InitiatorDto>> Update(int id, UpdateInitiatorRequestModel model)
        {
            var initiator = await _initiatorRepository.Get(id);
            if (initiator == null) return new BaseResponse<InitiatorDto>
            {
                Message = "User not found",
                Status = false,
            };

            var userImage = "";
            if (model.Image != null)
            {
                string imagePath = Path.Combine(_webroot.WebRootPath, "UserImage");
                Directory.CreateDirectory(imagePath);
                string userImageType = model.Image.ContentType.Split('/')[1];
                userImage = $"{Guid.NewGuid()}.{userImageType}";
                var rightpath = Path.Combine(imagePath, userImage);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.Image.CopyTo(filestream);
                }
            }

            initiator.User.FirstName = model.FirstName;
            initiator.User.LastName = model.LastName;
            initiator.User.Address = model.Address;
            initiator.User.PhoneNumber = model.PhoneNumber;
            initiator.User.Image = userImage;

            await _initiatorRepository.Update(initiator);

            return new BaseResponse<InitiatorDto>
            {
                Message = "Successfully Updated",
                Status = true,
                Data = new InitiatorDto
                {
                    FirstName = initiator.User.FirstName,
                    Email = initiator.User.Email,
                }
            };
        }
    }
}
