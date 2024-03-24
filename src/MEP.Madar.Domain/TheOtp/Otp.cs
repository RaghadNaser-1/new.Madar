using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace MEP.Madar.TheOtp
{
    public class Otp : IEntity<int>
    {
        public int Id { get; set; } // Assuming you have an Id property for the Otp entity
        //public Guid UserId { get; set; }
        public int UserOtp { get; set; }

        public Guid AbpUserId { get; set; }
        public DateTime OtpExpTime { get; set; }

        public IdentityUser AbpUser { get; set; } // Navigation property referencing AbpUser entity

        //public int Id => throw new NotImplementedException();

      

        public object?[] GetKeys()
        {
            throw new NotImplementedException();
        }
    }
}
