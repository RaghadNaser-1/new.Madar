using MEP.Madar.Helper.Dto;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MEP.Madar.Helper
{
    public interface IOtpService : IApplicationService
    {
        //Task<string> GenerateAndSendOtpAsync(string email);
        //Task<bool> ConfirmOtpAsync(string email, string otpString);
        //Task<bool> ForgotPassword(string Email, string url);
        
        Task<string> GenerateAndSendOtpAsync(string email);
        Task<bool> ResetPassword(ResetPasswordDto model);
        Task<string> ConfirmOtpAsync(string email, string otp);
       

        //Task<bool> ConfirmOtp(string email, short otpString);


    }
}
