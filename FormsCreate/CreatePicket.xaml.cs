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
    public partial class CreatePicket : Window
    {
        public CreatePicket()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            NumberEquipment1();
            Order1();
            Methodology1();
            Type1();
        }

        private void Type1() // Заполнение данными в ComboBox
        {
            txtTypeMeasurement.ItemsSource = new string[] { "Рядовое", "Контрольное", "Опытное" };
        }

        private void NumberEquipment1() //Собирает [Номер оборудования] которые существуют в сущности Proekt.Оборудования на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер оборудования] FROM Proekt.Оборудования";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtNumberEquipment.Items.Clear();
            while (reader.Read())
            {
                txtNumberEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Methodology1() //Собирает [Номер методики] которые существуют в сущности Proekt.Методика на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер методики] FROM Proekt.Методика";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtNumberMethodology.Items.Clear();
            while (reader.Read())
            {
                txtNumberMethodology.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Order1()//Собирает [Номер отряда] которые существуют в сущности Proekt.[Полевой отряд] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер отряда] FROM Proekt.[Полевой отряд]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtNumberOrder.Items.Clear();
            while (reader.Read())
            {
                txtNumberOrder.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into  Proekt.Пикет([Наименование пикета],[Координата],[Вид измерения],[Номер отряда],[Номер методики],[Номер оборудования])" +
                $"values ('{txtNamePicket.Text}','{txtCoordinate.Text}','{txtTypeMeasurement.Text}', '{txtNumberOrder.Text}','{txtNumberMethodology.Text}','{txtNumberEquipment.Text}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Order1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Methodology1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberEquipment1();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }


        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateMethodology tv = new CreateMethodology();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateOrder tv = new CreateOrder();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateEquipment tv = new CreateEquipment();
            tv.ShowDialog();
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberMet met = new NumberMet();
            met.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberOrder met = new NumberOrder();
            met.ShowDialog();
        }
        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberEq met = new NumberEq();
            met.ShowDialog();
        }
    }
}
