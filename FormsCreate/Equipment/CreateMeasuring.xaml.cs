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

namespace WindowRBD1.FormsCreate.Equipment
{
    public partial class CreateMeasuring : Window
    {
        public CreateMeasuring()
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

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "INSERT INTO Proekt.[Измерительное оборудование]([Наименование],[Инвентарный номер],[Дата приобретения],[Дата поверки],[Характеристики]) VALUES(@txtName, @txtInventory, @dateVerification, @datePurchases, @txtCharacteristic)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txtName", txtName.Text);
                cmd.Parameters.AddWithValue("@txtInventory", txtInventory.Text);
                cmd.Parameters.AddWithValue("@dateVerification", dateVerification.SelectedDate);
                cmd.Parameters.AddWithValue("@datePurchases", datePurchases.SelectedDate);
                cmd.Parameters.AddWithValue("@txtCharacteristic", txtCharacteristic.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }
    }
}
