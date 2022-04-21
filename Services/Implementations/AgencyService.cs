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
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IWebHostEnvironment _webroot;

        public AgencyService(IAgencyRepository agencyRepository, IWebHostEnvironment webroot)
        {
            _agencyRepository = agencyRepository;
            _webroot = webroot;
        }

        public async Task<BaseResponse<AgencyDto>> Create(CreateAgencyRequestModel model)
        {
            var agencyExist = await _agencyRepository.Get(a => a.Name == model.Name);
            if (agencyExist != null) return new BaseResponse<AgencyDto>
            {
                Message = "Agency already exist",
                Status = false,
            };

            var agencyLogo = "";
            if (model.Logo != null)
            {
                string agencyLogoPpath = Path.Combine(_webroot.WebRootPath, "AgencyLogo");
                Directory.CreateDirectory(agencyLogoPpath);
                string AdminPhotoType = model.Logo.ContentType.Split('/')[1];
                agencyLogo = $"{Guid.NewGuid()}.{AdminPhotoType}";
                var rightpath = Path.Combine(agencyLogoPpath, agencyLogo);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.Logo.CopyTo(filestream);
                }
            }

            var agency = new Agency
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                RegistrationNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),
                Description = model.Description,
                DisasterType = model.DisasterType,
                Logo = agencyLogo,

            };

            await _agencyRepository.Create(agency);

            return new BaseResponse<AgencyDto>
            {
                Message = "Agency created successfully",
                Status = true,
                Data = new AgencyDto
                {
                    Name = agency.Name,
                }
            };
        }

        public async Task<BaseResponse<AgencyDto>> Delete(int id)
        {
            var agency = await _agencyRepository.Get(id);
            if (agency == null) return new BaseResponse<AgencyDto>
            {
                Message = "Role Not Found",
                Status = false,
            };

            agency.IsDeleted = true;
            _agencyRepository.Save();

            return new BaseResponse<AgencyDto>
            {
                Message = "Delete Successful",
                Status = true,
            };
        }

        public async Task<BaseResponse<AgencyDto>> Get(int id)
        {
            var agency = await _agencyRepository.Get(id);
            if (agency == null) return new BaseResponse<AgencyDto>
            {
                Message = "Agency not found",
                Status = false,
            };
            return new BaseResponse<AgencyDto>
            {
                Message = "Successful",
                Status = true,
                Data = new AgencyDto
                {
                    Id = agency.Id,
                    Name = agency.Name,
                    Abbreviation = agency.Abbreviation,
                    RegistrationNumber = agency.RegistrationNumber,
                    DisasterType = agency.DisasterType,
                    Description = agency.Description,
                    Logo = agency.Logo,
                    Branches = agency.Branches.Select(a => new BranchDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        ReferenceNumber = a.ReferenceNumber,
                        Address = a.Address,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,

                    }).ToList(),

                   /* Staffs = agency.Staffs.Select(b => new StaffDto
                    {
                        Id = b.Id,
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        EmploymentNumber = b.EmploymentNumber,
                        PhoneNumber = b.User.PhoneNumber,
                        Roles = b.User.UserRoles.Select(c => new RoleDto
                        {
                            Id = c.Id,
                            Name = c.Role.Name,
                        }).ToList(),
                    }).ToList(),*/
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<AgencyDto>>> GetAll()
        {
            var agencies = await _agencyRepository.GetAll();
            return new BaseResponse<IEnumerable<AgencyDto>>
            {
                Data = agencies.Select(agency => new AgencyDto
                {
                    Id = agency.Id,
                    Name = agency.Name,
                    Abbreviation = agency.Abbreviation,
                    RegistrationNumber = agency.RegistrationNumber,
                    DisasterType = agency.DisasterType,
                    Description = agency.Description,
                    Logo = agency.Logo,
                }),
                Message = "Successful",
                Status = true,

            };
           /* var listOfAgencies = agencies.Select(agency => new AgencyDto
            {
                Id = agency.Id,
                Name = agency.Name,
                Abbreviation = agency.Abbreviation,
                RegistrationNumber = agency.RegistrationNumber,
                DisasterType = agency.DisasterType,
                Description = agency.Description,
                Logo = agency.Logo,
                *//*Branches = agency.Branches.Select(a => new BranchDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    ReferenceNumber = a.ReferenceNumber,
                    Address = a.Address,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNumber,

                }).ToList(),

                Staffs = agency.Staffs.Select(b => new StaffDto
                {
                    Id = b.Id,
                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName,
                    EmploymentNumber = b.EmploymentNumber,
                    PhoneNumber = b.User.PhoneNumber,
                    Roles = b.User.UserRoles.Select(c => new RoleDto
                    {
                        Id = c.Id,
                        Name = c.Role.Name,
                    }).ToList(),
                }).ToList(),*//*
            });
            return new BaseResponse<IEnumerable<AgencyDto>>
            {
                Message = "Successful",
                Status = true,
                Data = listOfAgencies,
            };*/
        }

        public async Task<BaseResponse<AgencyDto>> Update(int id, UpdateAgencyRequestModel model)
        {
            var agency = await _agencyRepository.Get(id);

            if (agency == null) return new BaseResponse<AgencyDto>
            {
                Message = "Agency not found",
                Status = false,
            };

            var agencyLogo = "";
            if (model.Logo != null)
            {
                string agencyLogoPpath = Path.Combine(_webroot.WebRootPath, "AgencyLogo");
                Directory.CreateDirectory(agencyLogoPpath);
                string AdminPhotoType = model.Logo.ContentType.Split('/')[1];
                agencyLogo = $"{Guid.NewGuid()}.{AdminPhotoType}";
                var rightpath = Path.Combine(agencyLogoPpath, agencyLogo);
                using (var filestream = new FileStream(rightpath, FileMode.Create))
                {
                    model.Logo.CopyTo(filestream);
                }
            }

            agency.Name = model.Name;
            agency.Abbreviation = model.Abbreviation;
            agency.Description = model.Description;
            agency.DisasterType = model.DisasterType;
            agency.Logo = agencyLogo;

            await _agencyRepository.Update(agency);
            return new BaseResponse<AgencyDto>
            {
                Message = "Updated Succesfully",
                Status = true,
                Data = new AgencyDto
                {
                    Id = agency.Id,
                    Name = agency.Name,
                    Abbreviation = agency.Abbreviation,
                    DisasterType = agency.DisasterType,
                }
            };


        }
    }
}
