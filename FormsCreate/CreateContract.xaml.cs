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
using WindowRBD1.Class;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateContract : Window
    {
        public CreateContract()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.Договор([Наименование договора],[Начала],[Окончания],[Стоимость],[Дата и время появления записи])" +
                $"values ('{txtNameContract.Text}','{txtBeginnings.SelectedDate.Value.ToString("dd-MM-yyyy")}','{txtEndings.SelectedDate.Value.ToString("dd-MM-yyyy")}', '{txtCost.Text}', '{Time.Content}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }
    }
}
