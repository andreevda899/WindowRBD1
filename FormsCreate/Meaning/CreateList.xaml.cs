using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.MonthCalendar;
using WindowRBD1.Class;

namespace WindowRBD1.FormsCreate.Meaning
{
    /// <summary>
    /// Логика взаимодействия для CreateList.xaml
    /// </summary>
    public partial class CreateList : Window
    {
        public CreateList()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into Proekt.[Список координат углов периметра]([x1],[y1],[x2],[y2],[x3],[y3])" +
                $"values ('{txtX1.Text}','{txtY1.Text}','{txtX2.Text}','{txtY2.Text}', '{txtX3.Text}','{txtY3.Text}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }
    }
}
