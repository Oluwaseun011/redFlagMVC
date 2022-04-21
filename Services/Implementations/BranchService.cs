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
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IAgencyRepository _agencyRepository;
        public BranchService(IBranchRepository branchRepository, IAgencyRepository agencyRepository)
        {
            _branchRepository = branchRepository;
            _agencyRepository = agencyRepository;
        }
        public async Task<BaseResponse<BranchDto>> Create(CreateBranchRequestModel model, int agencyId)
        {
            var branchExist = await _branchRepository.Get(a => a.Email == model.Email);
            if (branchExist != null) return new BaseResponse<BranchDto>
            {
                Message = "Branch already exist",
                Status = false,
            };

            var agency = await _agencyRepository.Get(agencyId);

            var branch = new Branch
            {
                Name = model.Name,
                AgencyId = agencyId,
                Email = model.Email,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                PhoneNumber = model.PhoneNumber,
                ReferenceNumber = $"{agency.Abbreviation.Replace(".", "")}/{agency.Id:000}/{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7).ToUpper()}",

            };

            await _branchRepository.Create(branch);

            return new BaseResponse<BranchDto>
            {
                Message = "created successfully",
                Status = true,
                Data = new BranchDto
                {
                    Name = branch.Name,
                    ReferenceNumber = branch.ReferenceNumber,
                    Email = branch.Email,
                    Latitude = branch.Latitude,
                    Longitude = branch.Longitude,
                }
            };
        }

        public async Task<BaseResponse<BranchDto>> CreateHeadquaters(CreateHeadquatersRequestModel model, int agencyId)
        {
            var branchExist = await _branchRepository.Get(a => a.Email == model.Email);
            if (branchExist != null) return new BaseResponse<BranchDto>
            {
                Message = "Branch already exist",
                Status = false,
            };

            var agency = await _agencyRepository.Get(agencyId);

            var branch = new Branch
            {
                Name = agency.Name + " Headquarters",
                AgencyId = agencyId,
                Email = model.Email,
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                PhoneNumber = model.PhoneNumber,
                ReferenceNumber = $"{agency.Abbreviation.Replace(".", "")}/{agency.Id:000}/{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7).ToUpper()}",

            };

            await _branchRepository.Create(branch);

            return new BaseResponse<BranchDto>
            {
                Message = "created successfully",
                Status = true,
                Data = new BranchDto
                {
                    Name = branch.Name,
                    ReferenceNumber = branch.ReferenceNumber,
                    Email = branch.Email,
                    Latitude = branch.Latitude,
                    Longitude = branch.Longitude,
                }
            };
        }

        public async Task<BaseResponse<BranchDto>> Delete(int id)
        {
            var branch = await _branchRepository.Get(id);
            if (branch == null) return new BaseResponse<BranchDto>
            {
                Message = "Role Not Found",
                Status = false,
            };
            branch.IsDeleted = true;
            _branchRepository.Save();
            return new BaseResponse<BranchDto>
            {
                Message = "Delete Successful",
                Status = true,
            };
        }

        public async Task<BaseResponse<BranchDto>> Get(int id)
        {
            var branch = await _branchRepository.Get(id);
            if (branch == null) return new BaseResponse<BranchDto>
            {
                Message = "Branch does not exist",
                Status = false,
            };
            return new BaseResponse<BranchDto>
            {
                Message = "successful",
                Status = true,
                Data = new BranchDto
                {
                    Id = branch.Id,
                    Name = branch.Name,
                    ReferenceNumber = branch.ReferenceNumber,
                    Email = branch.Email,
                    Address = branch.Address,
                    AgencyId = branch.AgencyId,
                    AgencyName = branch.Agency.Name,
                    PhoneNumber = branch.PhoneNumber,
                    Longitude = branch.Longitude,
                    Latitude = branch.Latitude,
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<BranchDto>>> GetAll()
        {
            var branches = await _branchRepository.GetAll();
            var listOfBranches = branches.ToList().Select(branch => new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                ReferenceNumber = branch.ReferenceNumber,
                Email = branch.Email,
                Address = branch.Address,
                AgencyId = branch.AgencyId,
                AgencyName = branch.Agency.Name,
                PhoneNumber = branch.PhoneNumber,
                Longitude = branch.Longitude,
                Latitude = branch.Latitude,
            });
            return new BaseResponse<IEnumerable<BranchDto>>
            {
                Message = "success",
                Status = true,
                Data = listOfBranches
            };
        }

        public async Task<BaseResponse<BranchDto>> Update(int id, UpdateBranchRequestModel model)
        {
            var branch = await _branchRepository.Get(id);
            if (branch == null) return new BaseResponse<BranchDto>
            {
                Message = "branch does not exist",
                Status = false,
            };

            branch.Name = model.Name;
            branch.Email = model.Email;
            branch.Address = model.Address;
            branch.Latitude = model.Latitude;
            branch.Longitude = model.Longitude;
            branch.PhoneNumber = model.PhoneNumber;

            await _branchRepository.Update(branch);

            return new BaseResponse<BranchDto>
            {
                Message = "Update successful",
                Status = true,
                Data = new BranchDto
                {
                    Name = branch.Name,
                    ReferenceNumber = branch.ReferenceNumber,
                    Email = branch.Email,
                    Address = branch.Address,
                }
            };

        }
    }
}
