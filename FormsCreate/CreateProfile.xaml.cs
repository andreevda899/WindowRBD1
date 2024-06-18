using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.Forms;
using WindowRBD1.FormsCreate.Works;

namespace WindowRBD1.FormsCreate
{
    public partial class CreateProfile : Window
    {
        public CreateProfile()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
            //
            txtNumberPicket1();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.Профили([Наименование профиля],[Номер пикета],[Координаты начала],[Координаты изломов],[Координаты окончания],[Длина],[Дата и время появления записи])" +
                $"values ('{txtNameProfile.Text}','{txtNumberPicket.Text}','{txtCoordinatesBeginning.Text}', '{txtCoordinatesFracture.Text}', '{txtCoordinatesEnd.Text}', '{txtLength.Text}','{Time.Content}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись добавлена");
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            txtNumberPicket1();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close(); 
        }

        private void txtNumberPicket1() //Собирает [Номер пикета] которые существуют в сущности Proekt.[Пикет] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер пикета] FROM Proekt.Пикет";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtNumberPicket.Items.Clear();
            while (reader.Read())
            {
                txtNumberPicket.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberPicket tv = new NumberPicket();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreatePicket tv = new CreatePicket();
            tv.ShowDialog();
        }

    }
}
