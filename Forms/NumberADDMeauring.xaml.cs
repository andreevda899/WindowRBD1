using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
using WindowRBD1.Class;

namespace WindowRBD1.Forms
{
    public partial class NumberADDMeauring : Window
    {
        public NumberADDMeauring()
        {
            InitializeComponent();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void btClose_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Close();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();

                string sql = "insert into  Proekt.[Описание измерительной установки]([Размер],[Разносы])" +
                $"values ('{Size}','{Postings}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }
    }
}
