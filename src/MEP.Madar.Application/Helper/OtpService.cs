using Azure.Core;
using MEP.Madar.Models.Auth;
using MEP.Madar.Models;

using MEP.Madar.TheOtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using MEP.Madar.Helper.Dto;

namespace MEP.Madar.Helper
{
    public class OtpService : ApplicationService, IOtpService
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly EmailService _emailService;
        //private readonly IOtpRepository _otpRepository; // Assuming IOtpRepository is the repository interface for the Otp entity
        private readonly IRepository<Otp> otpRepository;
        private readonly IConfiguration _configuration;

        //public OtpService(UserManager<IdentityUser> userManager, IOtpRepository otpRepository)
        public OtpService(UserManager<IdentityUser> userManager, IRepository<Otp> repository, IConfiguration configuration)
        {
            _userManager = userManager;
            otpRepository = repository;
            _configuration = configuration;
        }
        public async Task<string> ConfirmOtpAsync(string email, string otp)
        {

            if (string.IsNullOrEmpty(email))
                return "email is required!";

            bool parsedResult = short.TryParse(otp, out short Otp);
            if (string.IsNullOrEmpty(otp) || !parsedResult)
                return "Otp is invalid!";

            bool result = await ConfirmOtp(email, Otp);
            if (result)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return "Otp is invalid!";
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string baseUrl = _configuration["BaseURL"];
                var callbackUrl = $"api/app/otp/reset-password?UserId={user.Id}&token={token}";

                return baseUrl + callbackUrl;
            }
            else
                return "Otp is invalid!";
        }
        


        

        //return string link
        private async Task<bool> ConfirmOtp(string email, short otpString)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { return false; }
            var otpRecord = await otpRepository.FirstOrDefaultAsync(otp => otp.AbpUserId == user.Id);
            if (otpRecord == null)
            {
                return false; // OTP record not found
            }
            // Verify that the OTP matches the one provided by the user
            if (otpRecord.UserOtp != otpString)
            {
                return false; // OTP mismatch
            }

            // Check if the OTP has expired
            if (otpRecord.OtpExpTime < DateTime.Now)
            {
                return false; // OTP expired 
            }

            // OTP is valid and not expired
            return true;

        }

        public async Task<bool> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return true;
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                // Handle password reset failure
                return false;
            }
        }

        public async Task<bool> ForgotPassword(string Email, string url)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {

                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string prams = $"?userId={user.Id}&token={token}";
            url += prams;
            //url.Replace("resetpassword", user.Id+"").Replace("token", token);
            //await _emailService.SendEmailAsync(Email, "Reset Password",
            //    $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

            return true;
        }

        public async Task<string> GenerateAndSendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return string.Empty;
            var otp_string = GenerateOtp();
            if (!String.IsNullOrEmpty(otp_string))
            {
                var otpRecord = await otpRepository.FirstOrDefaultAsync(otp => otp.AbpUserId == user.Id);
                if (otpRecord == null)
                {
                    bool b = int.TryParse(otp_string, out int otp);
                    var otpEntity = new Otp
                    {
                        AbpUserId = user.Id,
                        UserOtp = otp,
                        OtpExpTime = DateTime.Now.AddMinutes(10) // Assuming OTP expires after 10 minutes
                    };
                    await otpRepository.InsertAsync(otpEntity);
                }
                else
                {
                    bool b = int.TryParse(otp_string, out int otp);
                    otpRecord.UserOtp = otp;
                    otpRecord.OtpExpTime = DateTime.Now.AddMinutes(10);
                    await otpRepository.UpdateAsync(otpRecord);
                }
            }

            return otp_string;
        }

        private string GenerateOtp()
        {
            int length = 4;
            const string validChars = "1234567890";
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            var otp = new char[length];
            for (int i = 0; i < length; i++)
            {
                otp[i] = validChars[randomBytes[i] % validChars.Length];
            }
            return new string(otp);
        }
    }
}
