using System.Collections.Concurrent;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using OTP_Verification_Project.Model;
using OtpNet;

namespace OTP_Verification_Project.Services
{
    public class OTPService
    {
        private readonly ConcurrentDictionary<string, Users> _userstore = new();
        public (string qrcode, string secretKey ) RegisterUser(string email)
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            var Base32Secret = Base32Encoding.ToString(key);

            var user = new Users
            {
                Email = email,
                SecretKey = Base32Secret
                
            };
            _userstore[email] = user;

            var urlOTP = $"otpauth://totp/OTP_Verification_App:{email}?secret={Base32Secret}&issuer=OTP_Verification_App";



            return (urlOTP, Base32Secret);


        }

        public bool VerifyOTP(string email ,string otp)
        {
            if (!_userstore.TryGetValue(email, out var user)) return false;
                var topt = new Totp(Base32Encoding.ToBytes(user.SecretKey));
                var isValid = topt.VerifyTotp(otp, out _, new VerificationWindow(2, 2));
                if (isValid) user.IsOtpVerifyed = true;
            return isValid;
            
        }

        public Users GetUsers(string email) => _userstore.GetValueOrDefault(email);

        


    }
}
