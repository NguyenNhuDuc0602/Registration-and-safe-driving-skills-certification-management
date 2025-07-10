using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System.Collections.Generic;
using System.Linq;
namespace Project_Prn.dal
{
    public class CertificateDAO
    {
        private Prngroup4Context dbc;
<<<<<<< HEAD
=======

>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
        public CertificateDAO()
        {
            dbc = new Prngroup4Context();
        }
<<<<<<< HEAD
=======

>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
        // 1. Lấy tất cả chứng chỉ
        public List<Certificate> GetAllCertificate()
        {
            return dbc.Certificates
<<<<<<< HEAD
                .Include(c => c.User) // Eager load thông tin User
=======
                .Include(c => c.User)
>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
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
