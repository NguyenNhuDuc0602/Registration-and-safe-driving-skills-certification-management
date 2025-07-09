using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System.Collections.Generic;
using System.Linq;
namespace Project_Prn.dal
{
    public class CertificateDAO
    {
        private Prngroup4Context dbc;

        public CertificateDAO()
        {
            dbc = new Prngroup4Context();
        }

        // 1. Lấy tất cả chứng chỉ
        public List<Certificate> GetAllCertificate()
        {
            return dbc.Certificates
                .Include(c => c.User)
                .ToList();
        }

        // 2. Lấy chứng chỉ theo ID
        public Certificate? GetByIdCertificate(int certificateId)
        {
            return dbc.Certificates
                .Include(c => c.User)
                .FirstOrDefault(c => c.CertificateId == certificateId);
        }

        // 3. Thêm mới chứng chỉ
        public void AddCertificate(Certificate certificate)
        {
            dbc.Certificates.Add(certificate);
            dbc.SaveChanges();
        }

        // 4. Cập nhật chứng chỉ
        public void UpdateCertificate(Certificate certificate)
        {
            dbc.Certificates.Update(certificate);
            dbc.SaveChanges();
        }

        // 5. Xóa chứng chỉ
        public void DeleteCertificate(int certificateId)
        {
            var certificate = dbc.Certificates.Find(certificateId);
            if (certificate != null)
            {
                dbc.Certificates.Remove(certificate);
                dbc.SaveChanges();
            }
        }

        // 6. Lấy chứng chỉ theo UserId
        public List<Certificate> GetByUserIdCertificate(int userId)
        {
            return dbc.Certificates
                .Where(c => c.UserId == userId)
                .ToList();
        }
       

    }
}
