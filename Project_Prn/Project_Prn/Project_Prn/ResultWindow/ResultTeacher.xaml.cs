<<<<<<< HEAD
﻿using System;
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

namespace Project_Prn.ResultWindow
{
    /// <summary>
    /// Interaction logic for ResultTeacher.xaml
    /// </summary>
    public partial class ResultTeacher : Window
    {
        public ResultTeacher()
        {
            InitializeComponent();
=======
﻿using Project_Prn.dal;
using Project_Prn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Prn.ResultWindow
{
    public partial class ResultTeacher : Window
    {
        private ResultDAO resultDAO;
        private List<Result> resultList;

        public ResultTeacher()
        {
            InitializeComponent();
            resultDAO = new ResultDAO();
            LoadAllResults();
        }

        private void LoadAllResults()
        {
            resultList = resultDAO.GetAllResult();
            dgResults.ItemsSource = resultList;
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtExamIdFilter.Text, out int examId))
            {
                var filtered = resultDAO.GetByExamIdResult(examId);
                dgResults.ItemsSource = filtered;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng Exam ID.");
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            txtExamIdFilter.Text = "";
            LoadAllResults();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Ép DataGrid cập nhật giá trị mới vào source
            dgResults.CommitEdit(DataGridEditingUnit.Row, true);
            dgResults.CommitEdit(); // dòng này cũng cần để đảm bảo chắc chắn binding đã cập nhật

            Button btn = sender as Button;
            if (btn?.Tag is Result updatedResult)
            {
                try
                {
                    resultDAO.UpdateResult(updatedResult);
                    MessageBox.Show("Cập nhật thành công!");
                    LoadAllResults(); // Tải lại để cập nhật UI
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
                }
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is Result r)
            {
                if (MessageBox.Show($"Xóa kết quả của {r.User.FullName}?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    resultDAO.DeleteResult(r.ResultId);
                    MessageBox.Show("Đã xóa.");
                    LoadAllResults();
                }
            }
>>>>>>> 1aee48a4477379607c2632efdc1c4d41d78b0c06
        }
    }
}
