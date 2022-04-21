using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string AgencyAbbreviation { get; set; }
        public string AgencyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }


    public class LoginUserRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
}
