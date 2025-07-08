namespace OTP_Verification_Project.Model
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string SecretKey { get; set; }

        public bool IsOtpVerifyed { get; set; } = false;
    }
}
