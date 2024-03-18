using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace MEP.Madar.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public short Otp { get; set; }
        public DateTime OtpExpTime { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
