using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceNumber { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class CreateBranchRequestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class CreateHeadquatersRequestModel
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class UpdateBranchRequestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
