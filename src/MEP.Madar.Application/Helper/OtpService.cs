using MEP.Madar.Models.Auth;
using MEP.Madar.TheOtp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace MEP.Madar.Helper
{
    public class OtpService : ApplicationService, IOtpService
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly EmailService _emailService;
        //private readonly IOtpRepository _otpRepository; // Assuming IOtpRepository is the repository interface for the Otp entity
        private readonly IRepository<Otp> otpRepository;
        //public OtpService(UserManager<IdentityUser> userManager, IOtpRepository otpRepository)
        public OtpService(UserManager<IdentityUser> userManager, IRepository<Otp> repository)
        {
            _userManager = userManager;
            otpRepository = repository;
            


        }
        
        public async Task<bool> ConfirmOtpAsync(string email, string otpString)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) { return false; }
            var otpRecord = await otpRepository.FirstOrDefaultAsync(otp => otp.AbpUserId == user.Id);
            if (otpRecord == null)
            {
                return false; // OTP record not found
            }
            // Verify that the OTP matches the one provided by the user
            if (otpRecord.UserOtp.ToString() != otpString)
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
            //if (user == null || user.Otp < 0)
            //{
            //    // Handle invalid OTP or user not found
            //    return false;
            //}
            //else
            //{
            //    if (DateTime.Now.Subtract(user.OtpExpTime).TotalMinutes > 10) // Checks if OTP is older than 10 minutes
            //    {
            //        return false; // OTP is expired
            //    }
            //    return otp == user.Otp;
            //}

            //Clear OTP after successful confirmation
            //user.Otp = 0;
            //await _userManager.UpdateAsync(user);
            //return true;
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
                bool b = int.TryParse(otp_string, out int otp);
                //user.Otp = otp;
                //user.OtpExpTime = DateTime.Now.AddMinutes(10);
                // await _userManager.UpdateAsync(user);
                //SendOTPEmail(email, otp_string);

                var otpEntity = new Otp
                {
                    AbpUserId = user.Id,
                    UserOtp = otp,
                    OtpExpTime = DateTime.Now.AddMinutes(10) // Assuming OTP expires after 10 minutes
                };
                await otpRepository.InsertAsync(otpEntity);

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
