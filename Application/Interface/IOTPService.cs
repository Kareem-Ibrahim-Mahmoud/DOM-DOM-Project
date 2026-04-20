using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IOTPService
    {
        public string GenerateOTP();
        public string HashOTP(string otp);
        public bool VerifyOTP(string otp, string hashedOtp);
    }
}
