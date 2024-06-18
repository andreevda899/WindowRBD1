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
    public partial class CreatePersonal : Window
    {
        public CreatePersonal()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
            Role1();
        }

        private void Role1() //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Role.ItemsSource = new string[] { "Оператор", "Супервайзер" }; ;
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
                string sql = "INSERT INTO Proekt.[Пользователи]([ФИО],[Логин],[Пароль],[Роль]) VALUES(@txtName, @Login, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txtName", txtName.Text);
                cmd.Parameters.AddWithValue("@Login", Login.Text);
                cmd.Parameters.AddWithValue("@Password", Password.Text);
                cmd.Parameters.AddWithValue("@Role", Role.SelectedItem);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }
    }
}
