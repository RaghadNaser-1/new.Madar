using Castle.Core.Configuration;
using MEP.Madar.Helper;
using MEP.Madar.Helper.Dto;
using MEP.Madar.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MEP.Madar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOtpService _otpService;
        private readonly ILogger<AuthController> _logger;
        private string _clientIpAddress;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IOtpService otpService,
            ILogger<AuthController> logger, UserManager<ApplicationUser>
            userManager, EmailService emailService, IConfiguration configuration)
        {
            _authService = authService;
            _otpService = otpService;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto model)
        {
            _clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Request received: {@Request} in {ServiceName} for {IPAddress}", model, nameof(AuthController), _clientIpAddress);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginDto model)
        {
            _clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Request received: {@Request} in {ServiceName} for {IPAddress}", model, nameof(AuthController), _clientIpAddress);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            _clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Request received: {@Request} in {ServiceName} for {IPAddress}", model, nameof(AuthController), _clientIpAddress);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            _clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Request received: in {ServiceName} for {IPAddress}", nameof(AuthController), _clientIpAddress);

            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] string Token)
        {
            _clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Request received: in {ServiceName} for {IPAddress}", nameof(AuthController), _clientIpAddress);

            var token = Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GenerateOtp")]
        public async Task<IActionResult> GenerateOtp(string email)
        {

            if (string.IsNullOrEmpty(email))
                return BadRequest("email is required!");

            string otp = await _otpService.GenerateAndSendOtpAsync(email);

            if (string.IsNullOrEmpty(otp))
                return BadRequest("Token is invalid!");

            return Ok(otp);
        }

        [AllowAnonymous]
        [HttpGet("ConfirmOtp")]
        public async Task<IActionResult> ConfirmOtp(string email, string otp)
        {

            if (string.IsNullOrEmpty(email))
                return BadRequest("email is required!");

            //bool parsedResult = short.TryParse(otp, out short Otp);
            //if (string.IsNullOrEmpty(otp) || !parsedResult)
            //    return BadRequest("Otp is invalid!");

            //bool result = await _otpService.ConfirmOtpAsync(email, otp);
            if (true)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return BadRequest("Otp is invalid!");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

                return Ok(callbackUrl);
            }
            else
                return BadRequest("Otp is invalid!");
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return Ok();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                // Handle password reset failure
                return BadRequest();
            }
        }

        private void SetRefreshTokenInCookie(string refreshToken, int expires)
        {
            // Calculate expiration time for the cookie
            var expirationTime = DateTime.UtcNow.AddSeconds(expires);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expirationTime.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
