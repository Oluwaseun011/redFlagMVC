using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class InitiatorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public IList<ReportDto> Reports { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }

    public class CreateInitiatorRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class MiniCreateInitiatorRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateInitiatorRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
}
