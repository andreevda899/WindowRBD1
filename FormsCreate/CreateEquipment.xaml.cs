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
using WindowRBD1.FormsCreate.Equipment;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateEquipment : Window
    {
        public CreateEquipment()
        {
            InitializeComponent();

	 //Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            Generative1();
            Measuring1();
            Telemetry1();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void Generative1() //Собирает [Номер генеративного оборудования] которые существуют в сущности Proekt.[Генеративное оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер генеративного оборудования] FROM Proekt.[Генеративное оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbGenerative.Items.Clear();
            while (reader.Read())
            {
                cmbGenerative.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }
        private void Measuring1() //Собирает [Номер измерительного оборудования] которые существуют в сущности Proekt.[Измерительное оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер измерительного оборудования] FROM Proekt.[Измерительное оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbMeasuring.Items.Clear();
            while (reader.Read())
            {
                cmbMeasuring.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Telemetry1() //Собирает [Номер телеметрического оборудования] которые существуют в сущности Proekt.[Телеметрическое оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер телеметрического оборудования] FROM Proekt.[Телеметрическое оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbTelemetry.Items.Clear();
            while (reader.Read())
            {
                cmbTelemetry.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Generative1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Measuring1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Telemetry1();
        }

        private void btClose_Click(object sender, EventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btCreate_Click(object sender, EventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.Оборудования([Номер генеративного оборудования],[Номер измерительного оборудования],[Номер телеметрического оборудования])" +
                $"values ('{cmbGenerative.SelectedItem}','{cmbMeasuring.SelectedItem}','{cmbTelemetry.SelectedItem}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер генеративного оборудования],[Наименование] from Proekt.[Генеративное оборудование]";

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

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер измерительного оборудования],[Наименование] from Proekt.[Измерительное оборудование]";

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

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер телеметрического оборудования],[Наименование] from Proekt.[Телеметрическое оборудование]";

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

        private void btPoisk4_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер Оборудования],[Наименование] from Proekt.Оборудования";

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

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateMeasuring tv = new CreateMeasuring();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateGenerative tv = new CreateGenerative();
            tv.ShowDialog();
        }

        private void btAdd4_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateTelemetry tv = new CreateTelemetry();
            tv.ShowDialog();
        }
    }
}
