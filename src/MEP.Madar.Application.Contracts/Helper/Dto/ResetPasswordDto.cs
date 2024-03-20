using System;
using System.Collections.Generic;
using System.Text;

namespace MEP.Madar.Helper.Dto
{
    public class ResetPasswordDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
