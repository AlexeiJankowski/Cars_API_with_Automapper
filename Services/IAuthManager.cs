using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMW_API.Dtos;

namespace BMW_API.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}