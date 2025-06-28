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

namespace Project_Prn.RegistrationWindow
{
    /// <summary>
    /// Interaction logic for RegistrationManagement.xaml
    /// </summary>
    public partial class RegistrationManagement : Window
    {
        public RegistrationManagement()
        {
            InitializeComponent();
            LoadRegistration();
        }

        public async Task LoadRegistration()
        {
            RegistrationDAO dao = new RegistrationDAO();
            var regis = dao.GetAllRegistration();
            this.dgRegistration.ItemsSource = regis;
        }
    }
}