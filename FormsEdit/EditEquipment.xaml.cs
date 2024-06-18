using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using WindowRBD1.Class;
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsCreate.Equipment;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace WindowRBD1.FormsEdit
{
    public partial class EditEquipment : Window
    {
        public EditEquipment()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = System.DateTime.Now.ToString(); };
            timer.Start();
	//
            cmbNumberEquipment1();
            cmbNumberTelemetryEquipment1();
            cmbNumberMesuringEquipment1();
            cmbNumberGenEquipment1();
        }

        public static bool flag = false;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            flag = true;

            if (flag == true)
            {
                cmbNumberEquipment.Text = "";
                flag = false;
            }
            cmbNumberEquipment1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            cmbNumberTelemetryEquipment1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            cmbNumberMesuringEquipment1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            cmbNumberGenEquipment1();
        }

        private void cmbNumberEquipment1() //Собирает [Номер Оборудования] которые существуют в сущности Proekt.Оборудования на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Оборудования] FROM Proekt.Оборудования";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbNumberEquipment.Items.Clear();
            while (reader.Read())
            {
                cmbNumberEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void cmbNumberGenEquipment1() //Собирает [Номер генеративного оборудования] которые существуют в сущности Proekt.[Генеративное оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер генеративного оборудования] FROM Proekt.[Генеративное оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbNumberGenEquipment.Items.Clear();
            while (reader.Read())
            {
                cmbNumberGenEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void cmbNumberMesuringEquipment1() //Собирает [Номер измерительного оборудования] которые существуют в сущности Proekt.[Измерительное оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер измерительного оборудования] FROM Proekt.[Измерительное оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbNumberMesuringEquipment.Items.Clear();
            while (reader.Read())
            {
                cmbNumberMesuringEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void cmbNumberTelemetryEquipment1() //Собирает [Номер Оборудования] которые существуют в сущности Proekt.Оборудования на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер телеметрического оборудования] FROM Proekt.[Телеметрическое оборудование]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            cmbNumberTelemetryEquipment.Items.Clear();
            while (reader.Read())
            {
                cmbNumberTelemetryEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            int cmbNumberEquipment2 = Convert.ToInt32(cmbNumberEquipment.SelectedItem);
            int cmbNumberGenEquipment2 = Convert.ToInt32(cmbNumberGenEquipment.SelectedItem);
            int cmbNumberMesuringEquipment2 = Convert.ToInt32(cmbNumberMesuringEquipment.SelectedItem);
            int cmbNumberTelemetryEquipment2 = Convert.ToInt32(cmbNumberTelemetryEquipment.SelectedItem);
            
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Оборудования SET [Номер генеративного оборудования] = @NumberGenEquipment, [Номер измерительного оборудования] = @NumberMesuringEquipment, [Номер телеметрического оборудования] = @NumberTelemetryEquipment where [Номер Оборудования] = @NumberEquipment", con);
                cmd.Parameters.AddWithValue("@NumberEquipment", cmbNumberEquipment2);
                cmd.Parameters.AddWithValue("@NumberGenEquipment", cmbNumberGenEquipment2);
                cmd.Parameters.AddWithValue("@NumberMesuringEquipment", cmbNumberMesuringEquipment2);
                cmd.Parameters.AddWithValue("@NumberTelemetryEquipment", cmbNumberTelemetryEquipment2);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
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
            string sql = "select * from Proekt.Оборудования";

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

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateEquipment tv = new CreateEquipment();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateGenerative tv = new CreateGenerative();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateMeasuring tv = new CreateMeasuring();
            tv.ShowDialog();
        }

        private void btAdd4_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateTelemetry tv = new CreateTelemetry();
            tv.ShowDialog();
        }

        private void cmbNumberEquipment_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (flag == false) { 
                int cmbNumberEquipment2 = Convert.ToInt32(cmbNumberEquipment.SelectedItem);

                string str = "Select * from Proekt.Оборудования  where [Номер Оборудования] = " + cmbNumberEquipment2;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);
                    cmbNumberGenEquipment.Text = dr.Rows[0][1].ToString();
                    cmbNumberMesuringEquipment.Text = dr.Rows[0][2].ToString();
                    cmbNumberTelemetryEquipment.Text = dr.Rows[0][3].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
