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
using Project_Prn.dal;

namespace Project_Prn.CertificateWindow
{
    /// <summary>
    /// Interaction logic for CertificateManagement.xaml
    /// </summary>
    public partial class CertificateManagement : Window
    {
        public CertificateManagement()
        {
            InitializeComponent();
            LoadCertificates();
        }

        public void LoadCertificates()
        {
            CertificateDAO dao = new CertificateDAO();
            var cers = dao.GetAllCertificate();
            this.dgCertificates.ItemsSource = cers;
        }
    }
}
