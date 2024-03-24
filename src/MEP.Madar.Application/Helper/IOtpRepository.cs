using MEP.Madar.TheOtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP.Madar.Helper
{
    public interface IOtpRepository
    {
        Task<Otp> GetByIdAsync(int id);
        Task InsertAsync(Otp otp);
        Task UpdateAsync(Otp otp);
        Task DeleteAsync(Otp otp);
        // Other methods as needed...
    }

}
