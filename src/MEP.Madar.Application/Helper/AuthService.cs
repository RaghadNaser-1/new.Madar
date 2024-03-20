using MEP.Madar.Helper.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP.Madar.Helper
{
    public class AuthService : IAuthService
    {
        public Task<string> AddRoleAsync(AddRoleDto model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> GetTokenAsync(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RegisterAsync(UserRegisterDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
