using iTextSharp.text;
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
using WindowRBD1.FormsCreate.Meaning;
using WindowRBD1.FormsCreate.Works;
using WindowRBD1.FormsEdit.Works;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateArea : Window
    {
        public CreateArea()
        {
            InitializeComponent();
	    //Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	    //
            txtSupervisorOrder1();
            txtSupervisorData1();
            txtProfile1();
            txtList1();
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
                string sql = "insert into Proekt.Площади([Наименование площади],[Номер профиля],[Номер списка углов периметра],[Длина периметра],[Величина площади окружённая периметром],[Дата и время появления записи],[Супервайзер полевых работ],[Супервайзер обработки данных])" +
                $"values ('{txtArea.Text}','{txtProfile.SelectedItem}','{txtList.SelectedItem}','{txtPerimeterLength.Text}', '{txtSizeArea.Text}','{Time.Content}','{txtSupervisorOrder.SelectedItem}','{txtSupervisorData.SelectedItem}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            } 
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            txtList1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            txtProfile1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            txtSupervisorOrder1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            txtSupervisorData1();
        }

        private void txtSupervisorOrder1() //Собирает [Номера Супервайзеров] которые существуют в сущности Proekt.Супервайзер на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Супервайзера] FROM Proekt.Супервайзер";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtSupervisorOrder.Items.Clear();
            while (reader.Read())
            {
                txtSupervisorOrder.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void txtList1() //Собирает [Номер списка углов периметра] которые существуют в сущности Proekt.[Список координат углов периметра] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер списка углов периметра] FROM Proekt.[Список координат углов периметра]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtList.Items.Clear();
            while (reader.Read())
            {
                txtList.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }


        private void txtProfile1() //Собирает [Номер профиля] которые существуют в сущности Proekt.Профили на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер профиля] FROM Proekt.Профили";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtProfile.Items.Clear();
            while (reader.Read())
            {
                txtProfile.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void txtSupervisorData1() //Собирает [Номера Супервайзеров] которые существуют в сущности Proekt.Супервайзер на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Супервайзера] FROM Proekt.Супервайзер";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtSupervisorData.Items.Clear();
            while (reader.Read())
            {
                txtSupervisorData.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btPoisk_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
          NumberProfile tv = new NumberProfile();
          tv.ShowDialog();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateProfile tv = new CreateProfile();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberList tv = new NumberList();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
           CreateList tv = new CreateList();
           tv.ShowDialog();
        }

        private void txtList1(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер списка углов периметра] FROM Proekt.[Список координат углов периметра]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtList.Items.Clear();
            while (reader.Read())
            {
                txtList.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btEdit1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            EditList rt = new EditList();
            rt.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateSupervisor tv = new CreateSupervisor();
            tv.ShowDialog();
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberSupervisor tv = new NumberSupervisor();
            tv.ShowDialog();
        }
    }
}
