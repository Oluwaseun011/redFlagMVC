using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using redFlag.Dtos;
using redFlag.Entities;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace redFlag.Services.Implementations
{
    public class StaffService: IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IWebHostEnvironment _webroot;

        public StaffService(IStaffRepository staffRepository, IUserRepository userRepository, IAgencyRepository agencyRepository, IRoleRepository roleRepository, IBranchRepository branchRepository, IWebHostEnvironment webroot)
        {
            _staffRepository = staffRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _agencyRepository = agencyRepository;
            _branchRepository = branchRepository;
            _webroot = webroot;
        }
        public async Task<BaseResponse<StaffDto>> Create(CreateStaffRequestModel model)
        {
            
            var staffExist = await _staffRepository.Get(a => a.User.Email == model.Email);
            if (staffExist != null) return new BaseResponse<StaffDto>
            {
                Message = "User already exist",
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

            var role = await _roleRepository.Get(b => b.Name == "Staff");

            var user = new User
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Image = userImage,
                UserRoles = new List<UserRole>()
            };


            var staff = new Staff
            {
                User = user,
                UserId = user.Id,
                EmploymentNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
                //AgencyId = loginStaff.AgencyId,
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
            await _staffRepository.Create(staff);

            return new BaseResponse<StaffDto>
            {
                Message = "created successful",
                Status = true,
                Data = new StaffDto
                {
                    FirstName = staff.User.FirstName,
                    Email = staff.User.Email,
                }
            };
        }

        public async Task<BaseResponse<StaffDto>> CreateAgencyAdmin(CreateAgencyAdminRequestModel model, int id)
        {
            //var loginUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var loginStaff = await _staffRepository.Get(a => a.User.Id == loginUserId);

            var staffExist = await _staffRepository.Get(a => a.User.Email == model.Email);
            if (staffExist != null) return new BaseResponse<StaffDto>
            {
                Message = "User already exist",
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

            var asd = new List<int>();
            var role = await _roleRepository.Get(b => b.Name == "Staff");
            var role2 = await _roleRepository.Get(b => b.Name == "AgencyAdmin");
            asd.Add(role.Id);
            asd.Add(role2.Id);

            var sdf = await _roleRepository.GetSelected(asd);

            var agency = await _agencyRepository.Get(id);
            var branch = await _branchRepository.Get(a => a.AgencyId == agency.Id);

            var user = new User
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Image = userImage,
                UserRoles = new List<UserRole>()
            };


            var staff = new Staff
            {
                User = user,
                UserId = user.Id,
                EmploymentNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
                AgencyId = agency.Id,
                BranchId = branch.Id,
            };

            foreach(var rol in sdf)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = rol.Id,
                    Role = rol,
                    User = user,
                };

                user.UserRoles.Add(userRole);
            }


            await _userRepository.Create(user);
            await _staffRepository.Create(staff);

            return new BaseResponse<StaffDto>
            {
                Message = "created successful",
                Status = true,
                Data = new StaffDto
                {
                    FirstName = staff.User.FirstName,
                    Email = staff.User.Email,
                }
            };
        }

        public async Task<BaseResponse<StaffDto>> Get(int id)
        {
            var staff = await _staffRepository.Get(id);
            if (staff == null) return new BaseResponse<StaffDto>
            {
                Message = "Staff not found",
                Status = false,
            };
            return new BaseResponse<StaffDto>
            {
                Message = "Success",
                Status = true,
                Data = new StaffDto
                {
                    Id = staff.Id,
                    UserId = staff.UserId,
                    FirstName = staff.User.FirstName,
                    LastName = staff.User.LastName,
                    Email = staff.User.Email,
                    PhoneNumber = staff.User.PhoneNumber,
                    Address = staff.User.Address,
                    EmploymentNumber = staff.EmploymentNumber,
                    Roles = staff.User.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,

                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<StaffDto>>> GetAll()
        {
            var staffs = await _staffRepository.GetAll();
            var listOfStaffs = staffs.ToList().Select(staff => new StaffDto
            {
                Id = staff.Id,
                UserId = staff.UserId,
                FirstName = staff.User.FirstName,
                LastName = staff.User.LastName,
                Email = staff.User.Email,
                PhoneNumber = staff.User.PhoneNumber,
                Address = staff.User.Address,
                EmploymentNumber = staff.EmploymentNumber,
                Roles = staff.User.UserRoles.Select(a => new RoleDto
                {
                    Id = a.Id,
                    Name = a.Role.Name,
                    Description = a.Role.Description,

                }).ToList(),
            });

            return new BaseResponse<IEnumerable<StaffDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfStaffs,
            };
        }

        public async Task<BaseResponse<StaffDto>> Update(int id, UpdateStaffRequestModel model)
        {
            var staff = await _staffRepository.Get(id);
            if (staff == null) return new BaseResponse<StaffDto>
            {
                Message = "staff not found",
                Status = false,
            };

            staff.User.FirstName = model.FirstName;
            staff.User.LastName = model.LastName;
            staff.User.PhoneNumber = model.PhoneNumber;
            staff.User.Address = model.Address;

            await _staffRepository.Update(staff);

            return new BaseResponse<StaffDto>
            {
                Message = "success",
                Status = true,
                Data = new StaffDto
                {
                    EmploymentNumber = staff.EmploymentNumber,
                }
            };
        }
    }
}
