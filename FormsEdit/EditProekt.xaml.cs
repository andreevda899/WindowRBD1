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
using WindowRBD1.FormsCreate;

namespace WindowRBD1.FormsEdit
{
    public partial class EditProekt : Window
    {
        public EditProekt()
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
            NumberProekt1();
            NumberContract1();
        }

        public static bool flag = false;

        private void NumberProekt1()  //Собирает [Номер проекта] которые существуют в сущности Proekt.Проект на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер проекта] FROM Proekt.Проект";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            Number.Items.Clear();
            while (reader.Read())
            {
                Number.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void NumberClient1()  //Собирает [Номер заказчика] которые существуют в сущности Proekt.Заказчик на Sql Server
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

        private void NumberContract1()  //Собирает [Номер договора] которые существуют в сущности Proekt.Договор на Sql Server
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

        private void NumberArea1()  //Собирает [Номер площади] которые существуют в сущности Proekt.Площади на Sql Server
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

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            int NumberClient2 = Convert.ToInt32(NumberClient.SelectedItem);
            int NumberArea2 = Convert.ToInt32(NumberArea.SelectedItem);
            int NumberContract2 = Convert.ToInt32(NumberContract.SelectedItem);
            
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Проект SET [Название проекта] = @NameProekt,[Номер площади]=@NumberArea,[Номер заказчика]=@NumberClient,[Номер договора]=@NumberContract where [Номер проекта] = @Number", con);
                cmd.Parameters.AddWithValue("@NumberClient", NumberClient2);
                cmd.Parameters.AddWithValue("@NumberArea", NumberArea2);
                cmd.Parameters.AddWithValue("@NumberContract", NumberContract2);
                cmd.Parameters.AddWithValue("@Number", Number.SelectedItem);
                cmd.Parameters.AddWithValue("@NameProekt", NameProekt.Text);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            flag = true;

            if (flag == true)
            {
                Number.Text = "";
                flag = false;
            }

            NumberProekt1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberClient1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberContract1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            NumberArea1();
        }

        private void NameProekt_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false) { 
                string str = "Select * from Proekt.Проект  where [Номер проекта] = " + Number.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);

                    NameProekt.Text = dr.Rows[0][1].ToString();
                    NumberClient.Text = dr.Rows[0][2].ToString();
                    NumberContract.Text = dr.Rows[0][3].ToString();
                    NumberArea.Text = dr.Rows[0][4].ToString();

                    da.Dispose();
                    conn.Close();
                }
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

        private void btPoisk4_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberProekt tv = new NumberProekt();
            tv.ShowDialog();
        }

        private void btAdd4_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateProekt tv = new CreateProekt();
            tv.ShowDialog();
        }
    }
}
