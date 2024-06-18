using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsCreate.Works;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowRBD1.FormsEdit { 
    public partial class EditOrder : System.Windows.Window
    {
        public EditOrder()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            NumberOrder1();
            FioChief1();
            FioWorker1();
            FioDriver1();
            FioSupervisor1();
            FioITR1();
        }

        public static bool flag = false;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
       
        private void NumberOrder1() //Собирает [Номер отряда] которые существуют в сущности Proekt.[Полевой отряд] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер отряда] FROM Proekt.[Полевой отряд]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberOrder.Items.Clear();
            while (reader.Read())
            {
                NumberOrder.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            flag = true;

            if (flag == true)
            {
                NumberOrder.Text = "";
                flag = false;
            }

            NumberOrder1();
        }
        private void btRefresh1_Click(object sender, RoutedEventArgs e)
        {
            FioChief1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e)
        {
            FioITR1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e)
        {
            FioDriver1();
        }

        private void btRefresh4_Click(object sender, RoutedEventArgs e)
        {
            FioWorker1();
        }

        private void btRefresh5_Click(object sender, RoutedEventArgs e)
        {
            FioSupervisor1();
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

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            int NumberOrder2 = Convert.ToInt32(NumberOrder.SelectedItem);
            int FioWorker2 = Convert.ToInt32(FioWorker.SelectedItem);
            int FioITR2 = Convert.ToInt32(FioITR.SelectedItem);
            int FioSupervisor2 = Convert.ToInt32(FioSupervisor.SelectedItem);
            int FioDriver2 = Convert.ToInt32(FioDriver.SelectedItem);
            int FioChief2 = Convert.ToInt32(FioChief.SelectedItem);
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.[Полевой отряд] SET [Номер Начальника] = @FioChief, [Номер ИТР] = @FioITR, [Номер Водителя] = @FioDriver, [Номер Супервайзера] = @FioSupervisor, [Номер Рабочего] =@FioWorker where [Номер отряда] = @NumberOrder", con);
                cmd.Parameters.AddWithValue("@NumberOrder", NumberOrder2);
                cmd.Parameters.AddWithValue("@FioChief", FioChief2);
                cmd.Parameters.AddWithValue("@FioDriver", FioDriver2);
                cmd.Parameters.AddWithValue("@FioITR", FioITR2);
                cmd.Parameters.AddWithValue("@FioSupervisor", FioSupervisor2);
                cmd.Parameters.AddWithValue("@FioWorker", FioWorker2);
                
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void NumberOrder_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false) { 
                int NumberOrder2 = Convert.ToInt32(NumberOrder.SelectedItem);

                string str = "Select * from Proekt.[Полевой отряд]  where [Номер отряда] = " + NumberOrder2;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);

                    FioChief.Text = dr.Rows[0][1].ToString();
                    FioITR.Text = dr.Rows[0][2].ToString();
                    FioDriver.Text = dr.Rows[0][3].ToString();
                    FioWorker.Text = dr.Rows[0][4].ToString();
                    FioSupervisor.Text = dr.Rows[0][5].ToString();

                    da.Dispose();
                    conn.Close();
                }
            }
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

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select * from Proekt.[Полевой отряд]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.[Полевой отряд]");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Полевой отряд]"].DefaultView;
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

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateOrder tv = new CreateOrder();
            tv.ShowDialog();
        }

        private void btAdd6_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateSupervisor tv = new CreateSupervisor();
            tv.ShowDialog();
        }
    }
}
