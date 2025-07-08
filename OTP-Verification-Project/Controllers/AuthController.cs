using Microsoft.AspNetCore.Mvc;
using OTP_Verification_Project.Model;
using OTP_Verification_Project.Services;

namespace OTP_Verification_Project.Controllers
{
    [ApiController]
    [Route("api/otp")]
    public class AuthController : ControllerBase
    {
        private readonly OTPService _oTPService;

        public AuthController(OTPService  otpservice)
        {
            _oTPService = otpservice;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterOTP rotp)
        {
            var (qr, secret) = _oTPService.RegisterUser(rotp.Email)
            ;
            return Ok(new { qrCodeurl=qr,secretkey = secret });
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] VerifyOTP votp)
        {
            var success =  _oTPService.VerifyOTP(votp.Email, votp.OtpCode);
            if (!success) return BadRequest("OTP verification Failed!!");
            return Ok("OTP successfully Verified");
        }

    }
}
