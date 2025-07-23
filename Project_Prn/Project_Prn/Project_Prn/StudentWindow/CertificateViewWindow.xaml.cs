using Microsoft.EntityFrameworkCore;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Prn.StudentWindow
{
    public partial class CertificateViewWindow : Window
    {
        private readonly User currentUser;
        private readonly Prngroup4Context context = new();

        public CertificateViewWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadCertificates();
        }

        private void LoadCertificates()
        {
            var certs = context.Certificates
                .Where(c => c.UserId == currentUser.UserId)
                .Include(c => c.Course)
                .Select(c => new
                {
                    c.CertificateCode,
                    CourseName = c.Course.CourseName,
                    IssuedDate = c.IssuedDate.ToString("dd/MM/yyyy"),
                    ExpirationDate = c.ExpirationDate.ToString("dd/MM/yyyy")
                })
                .ToList();

            certificateDataGrid.ItemsSource = certs;
        }

    }
}