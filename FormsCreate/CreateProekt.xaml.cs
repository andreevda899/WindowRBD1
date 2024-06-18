using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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
    public partial class CreateProekt : Window
    {
        public CreateProekt()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            NumberArea1();
            NumberClient1();
            NumberContract1();
        }

        private void NumberClient1() //Собирает [Номер заказчика] которые существуют в сущности Proekt.[Заказчик] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер заказчика] FROM Proekt.Заказчик";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberClient.Items.Clear();
            while (reader.Read())
            {
                NumberClient.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void NumberContract1() //Собирает [Номер договора] которые существуют в сущности Proekt.Договор на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер договора] FROM Proekt.Договор";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberContract.Items.Clear();
            while (reader.Read())
            {
                NumberContract.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void NumberArea1()//Собирает [Номер площади] которые существуют в сущности Proekt.Площади на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер площади] FROM Proekt.Площади";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberArea.Items.Clear();
            while (reader.Read())
            {
                NumberArea.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberClient1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberContract1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberArea1();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            int NumberArea2 = Convert.ToInt32(NumberArea.SelectedItem);
            int NumberClient2 = Convert.ToInt32(NumberClient.SelectedItem);
            int NumberContract2 = Convert.ToInt32(NumberContract.SelectedItem);

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.Проект([Название проекта],[Номер заказчика],[Номер договора],[Номер площади],[Дата и время появления записи])" +
            $"values ('{txtProekt.Text}','{NumberClient2}', '{NumberContract2}', '{NumberArea2}','{Time.Content}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberPicket tv = new NumberPicket();
            tv.ShowDialog();
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreatePicket tv = new CreatePicket();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberClient tv = new NumberClient();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateClient tv = new CreateClient();
            tv.ShowDialog();
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberArea tv = new NumberArea();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateArea tv = new CreateArea();
            tv.ShowDialog();
        }
    }
}
