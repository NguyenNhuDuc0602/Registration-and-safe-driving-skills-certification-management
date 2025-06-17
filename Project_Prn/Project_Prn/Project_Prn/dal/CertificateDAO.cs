using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Prn.dal
{
    public class CertificateDAO
    {
        private Prngroup4Context dbc;
        public CertificateDAO() { dbc = new Prngroup4Context(); }
    }
    // 1. Lấy tất cả chứng chỉ
        public async Task<List<Certificate>> GetAllCertificateAsync()
        {
            return await dbc.Certificates
                .Include(c => c.User) // Eager load thông tin User
                .ToListAsync();
        }

        // 2. Lấy chứng chỉ theo ID
        public async Task<Certificate?> GetByIdCertificateAsync(int certificateId)
        {
            return await dbc.Certificates
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CertificateId == certificateId);
        }

        // 3. Thêm mới chứng chỉ
        public async Task AddCertificateAsync(Certificate certificate)
        {
            dbc.Certificates.Add(certificate);
            await dbc.SaveChangesAsync();
        }

        // 4. Cập nhật chứng chỉ
        public async Task UpdateCertificateAsync(Certificate certificate)
        {
            dbc.Certificates.Update(certificate);
            await dbc.SaveChangesAsync();
        }

        // 5. Xóa chứng chỉ
        public async Task DeleteCertificateAsync(int certificateId)
        {
            var certificate = await dbc.Certificates.FindAsync(certificateId);
            if (certificate != null)
            {
                dbc.Certificates.Remove(certificate);
                await dbc.SaveChangesAsync();
            }
        }

        // 6. Lấy chứng chỉ theo UserId
        public async Task<List<Certificate>> GetByUserIdCertificateAsync(int userId)
        {
            return await dbc.Certificates
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
