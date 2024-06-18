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
using WindowRBD1.FormsCreate.Works;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateOrder : Window
    {
        public CreateOrder()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	  //
            FioChief1();
            FioWorker1();
            FioDriver1();
            FioSupervisor1();
            FioITR1();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            int FioWorker2 = Convert.ToInt32(FioWorker.SelectedItem);
            int FioITR2 = Convert.ToInt32(FioITR.SelectedItem);
            int FioSupervisor2 = Convert.ToInt32(FioSupervisor.SelectedItem);
            int FioDriver2 = Convert.ToInt32(FioDriver.SelectedItem);
            int FioChief2 = Convert.ToInt32(FioChief.SelectedItem);
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.[Полевой отряд]([Номер Начальника],[Номер ИТР] ,[Номер Водителя],[Номер Рабочего],[Номер Супервайзера])" +
                $"values ('{FioChief2}','{FioITR2}','{FioDriver2}', '{FioWorker2}', '{FioSupervisor2}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            FioChief1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            FioITR1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            FioDriver1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            FioWorker1();
        }

        private void btRefresh4_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            FioSupervisor1();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void FioChief1() //Собирает [Номер Начальника] которые существуют в сущности Proekt.[Начальник отряда] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Начальника] FROM Proekt.[Начальник отряда]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            FioChief.Items.Clear();
            while (reader.Read())
            {
                FioChief.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void FioDriver1() //Собирает [Номер Водителя] которые существуют в сущности Proekt.[Водители] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Водителя] FROM Proekt.[Водители]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            FioDriver.Items.Clear();
            while (reader.Read())
            {
                FioDriver.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void FioITR1() //Собирает [Номер ИТР] которые существуют в сущности Proekt.[ИТР] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер ИТР] FROM Proekt.[ИТР]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            FioITR.Items.Clear();
            while (reader.Read())
            {
                FioITR.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void FioSupervisor1() //Собирает [Номер Супервайзера] которые существуют в сущности Proekt.[Супервайзер] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Супервайзера] FROM Proekt.[Супервайзер]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            FioSupervisor.Items.Clear();
            while (reader.Read())
            {
                FioSupervisor.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void FioWorker1() //Собирает [Номер Рабочего] которые существуют в сущности Proekt.[Рабочие] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Рабочего] FROM Proekt.[Рабочие]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            FioWorker.Items.Clear();
            while (reader.Read())
            {
                FioWorker.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();

        }


        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {   
            string sql = "select [Номер Начальника],ФИО from Proekt.[Начальник отряда]";
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.[Начальник отряда]");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Начальник отряда]"].DefaultView;
            }
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер ИТР],ФИО from Proekt.ИТР";
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.ИТР");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.ИТР"].DefaultView;
            }
        }

        private void btPoisk4_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер Водителя],ФИО from Proekt.Водители";
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.Водители");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.Водители"].DefaultView;
            }
        }

        private void btPoisk5_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер Рабочего],ФИО from Proekt.Рабочие";
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.Рабочие");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.Рабочие"].DefaultView;
            }
        }

        private void btPoisk6_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер Супервайзера],ФИО from Proekt.Супервайзер";
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.Супервайзер");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.Супервайзер"].DefaultView;
            }
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateChief tv = new CreateChief();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Просмотр данных
        {
            CreateITR tv = new CreateITR();
            tv.ShowDialog();
        }

        private void btAdd4_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateDriver tv = new CreateDriver();
            tv.ShowDialog();
        }

        private void btAdd5_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateWorker tv = new CreateWorker();
            tv.ShowDialog();
        }

        private void btAdd6_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateSupervisor tv = new CreateSupervisor();
            tv.ShowDialog();
        }
    }
}
