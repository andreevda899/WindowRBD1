using Org.BouncyCastle.Asn1.X509;
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

namespace WindowRBD1.FormsEdit.Equipment
{
    public partial class EditPersonal : Window
    {
        public EditPersonal()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            txtName1();
            Role1();
        }

        private void Role1() //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Role.ItemsSource = new string[] { "Оператор", "Супервайзер" }; ;
        }

        public static bool flag = false;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void txtName1() //Собирает [Номер пользователя] которые существуют в сущности Proekt.Пользователи на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер пользователя] FROM Proekt.Пользователи";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            txtNumber.Items.Clear();
            while (reader.Read())
            {
                txtNumber.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void txtName_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false) { 
                string str = "Select * from Proekt.[Пользователи]  where [Номер пользователя] = " + txtNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);
                    txtName.Text = dr.Rows[0][1].ToString();
                    Login.Text = dr.Rows[0][2].ToString();
                    Password.Text = dr.Rows[0][3].ToString();
                    Role.Text = dr.Rows[0][4].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            flag = true;

            if (flag == true)
            {
                txtNumber.Text = "";
                flag = false;
            }
            txtName1();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.[Пользователи] SET [ФИО] = @txtName,[Логин] = @Inventory,[Пароль]=@dateVerification,[Роль]=@datePurchases where [Номер пользователя] = @Number", con);
                cmd.Parameters.AddWithValue("@Number", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtName", txtName.Text);
                cmd.Parameters.AddWithValue("@Inventory", Login.Text);
                cmd.Parameters.AddWithValue("@dateVerification", Password.Text);
                cmd.Parameters.AddWithValue("@datePurchases", Role.SelectedItem);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btPoisk_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер пользователя],[ФИО],[Роль] from Proekt.[Пользователи]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.[Пользователи]");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Пользователи]"].DefaultView;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            CreatePersonal tv = new CreatePersonal();
            tv.ShowDialog();
        }
    }
}
