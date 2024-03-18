using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MEP.Madar.Helper
{
    public interface IOtpService : IApplicationService
    {
        Task<string> GenerateAndSendOtpAsync(string email);
        Task<bool> ConfirmOtpAsync(string email, int otp);
        Task<bool> ForgotPassword(string Email, string url);
    }
}
