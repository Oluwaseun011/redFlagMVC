using Microsoft.AspNetCore.Http;
using redFlag.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class StaffDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IList<RoleDto> Roles { get; set; }
        public string EmploymentNumber { get; set; }
    }

    public class CreateStaffRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class CreateAgencyAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class UpdateStaffRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
