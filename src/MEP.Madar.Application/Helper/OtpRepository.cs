using MEP.Madar.EntityFrameworkCore;
using MEP.Madar.TheOtp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEP.Madar.Helper
{
    public class OtpRepository : IOtpRepository
    {
        private readonly MadarDbContext _dbContext; // Replace YourDbContext with your actual DbContext

        public OtpRepository(MadarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Otp> GetByIdAsync(int id)
        {
            return await _dbContext.Otps.FindAsync(id);
        }

        public async Task InsertAsync(Otp otp)
        {
            _dbContext.Otps.Add(otp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Otp otp)
        {
            _dbContext.Entry(otp).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Otp otp)
        {
            _dbContext.Otps.Remove(otp);
            await _dbContext.SaveChangesAsync();
        }

        // Other methods as needed...
    }

}
