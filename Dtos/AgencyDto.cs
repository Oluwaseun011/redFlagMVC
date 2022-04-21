using Microsoft.AspNetCore.Http;
using redFlag.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class AgencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public DisasterType DisasterType { get; set; }
        public string Logo { get; set; }
        public IList<BranchDto> Branches { get; set; }
        public IList<StaffDto> Staffs { get; set; }
    }

    public class CreateAgencyRequestModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public DisasterType DisasterType { get; set; }
        public IFormFile Logo { get; set; }
    }

    public class UpdateAgencyRequestModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public DisasterType DisasterType { get; set; }
        public IFormFile Logo { get; set; }
    }
}

