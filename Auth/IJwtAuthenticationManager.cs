using redFlag.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Auth
{
    public interface IJwtAuthenticationManager
    {
        public string GenerateToken(UserDto user);
    }
}
