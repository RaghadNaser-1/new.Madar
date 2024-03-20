using MEP.Madar.Helper.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEP.Madar.Helper
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegisterDto model);
        Task<AuthResponseDto> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
        Task<AuthResponseDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
