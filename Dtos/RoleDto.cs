using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<UserDto> Users { get; set; }
    }

    public class CreateRoleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateRoleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
