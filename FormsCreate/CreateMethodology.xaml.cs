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
using WindowRBD1.Forms;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateMethodology : Window
    {
        public CreateMethodology()
        {
            InitializeComponent();

	    //Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();

            ListCategories1();
            ListCategories2();
        }

        private void BtClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();

                string sql = "insert into  Proekt.Методика([Наименование методики],[Описание генераторной установки],[Описание измерительной установки],[Описание телеметрической установки],[Продолжительность импульса],[Продолжительность паузы],[Сила тока])" +
                $"values ('{txtNameMethodology.Text}','{txtGenerative.SelectedItem}','{txtMeasuring.SelectedItem}', '{txtTelemetry.Text}', '{txtImpulse.Text}','{txtPause.Text}','{txtCurrent.Text}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }

        private void ListCategories1() //Собирает [Номер описания ген.установки] которые существуют в сущности Proekt.[Описание генераторной установки] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);

            string sql = "SELECT [Номер описания ген.установки] FROM Proekt.[Описание генераторной установки]";

            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            txtGenerative.Items.Clear();

            while (reader.Read())
            {
                txtGenerative.Items.Add(reader[0].ToString());

            }
            reader.Close();

            command.Dispose();
            connection.Close();
        }

        private void ListCategories2() //Собирает [Номер описания измер.установки] которые существуют в сущности Proekt.[Описание измерительной установки] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);

            string sql = "SELECT [Номер описания измер.установки] FROM Proekt.[Описание измерительной установки]";

            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            txtMeasuring.Items.Clear();

            while (reader.Read())
            {
                txtMeasuring.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command.Dispose();
            connection.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            ListCategories1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            ListCategories2();
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberADDGen tr = new NumberADDGen();
            tr.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberADDMeauring tr = new NumberADDMeauring();
            tr.ShowDialog();
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberGenerative tv = new NumberGenerative();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        { 
            NumberMeasuring tv = new NumberMeasuring();
            tv.ShowDialog();
        }
    }
}
