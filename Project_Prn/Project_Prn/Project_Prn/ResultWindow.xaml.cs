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

namespace Project_Prn
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private ResultDAO resultDAO=new ResultDAO();
        public ResultWindow()
        {
            InitializeComponent();
            LoadResult();
        }
        public void LoadResult()
        {
            var result = resultDAO.GetAllResult();
            dgResult.ItemsSource = result;
            CourseDAO courseDAO = new CourseDAO();
            var course = courseDAO.GetAllCourse();
            this.cbxCourse.ItemsSource = course;//combobox
            this.cbxCourse.DisplayMemberPath = "CourseName";
            this.cbxCourse.SelectedValuePath = "CourseId";
            this.cbxCourse.SelectedValue = 2;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
