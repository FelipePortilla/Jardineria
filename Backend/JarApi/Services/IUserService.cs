using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarApi.Dtos;

namespace JarApi.Services
{
    public interface IUserService
    {
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataUserDto> GetTokenAsync(LoginDto model);
    Task<string> AddRoleAsync(AddRoleDto model);
    Task<DataUserDto> RefreshTokenAsync(string refreshToken);
        
    }
}