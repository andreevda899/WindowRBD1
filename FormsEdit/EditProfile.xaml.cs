using iTextSharp.text.pdf;
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
    public partial class EditProfile : Window
    {
        public EditProfile()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            Picket1();
            NameProfile1();
        }

        public static bool flag = false;

        private void Picket1()  //Собирает [Номер пикета] которые существуют в сущности Proekt.Пикет на Sql Server
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

        private void NameProfile1()  //Собирает [Наименование профиля] которые существуют в сущности Proekt.Профили на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер профиля] FROM Proekt.Профили";
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

        private void txtNameProfile_SelectionChanged_1(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false) { 
                string str = "Select * from Proekt.Профили where [Номер профиля] = " + txtNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);

                    txtNameProfile.Text = dr.Rows[0][1].ToString();
                    txtNumberPicket.Text = dr.Rows[0][2].ToString();
                    txtCoordinatesBeginning.Text = dr.Rows[0][3].ToString();
                    txtCoordinatesFracture.Text = dr.Rows[0][4].ToString();
                    txtCoordinatesEnd.Text = dr.Rows[0][5].ToString();
                    txtLength.Text = dr.Rows[0][6].ToString();
                    dateBen.Text = dr.Rows[0][7].ToString();
                    dateEnd.Text = dr.Rows[0][8].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }

        private void btClose_Click_1(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            flag = true;

            if (flag == true)
            {
                txtNumber.Text = "";
                flag = false;
            }

            NameProfile1();

        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Picket1();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Профили SET [Наименование профиля] = @txtNameProfile,[Номер пикета]=@NumberPicket,[Координаты начала]=@CoordinatesBeginning,[Координаты изломов]=@CoordinatesFracture,[Координаты окончания]=@CoordinatesEnd,[Длина]=@Length,[Дата и время начала работ]=@dateBen,[Дата и время окончания работ]=@dateEnd,[Дата и время изменения записи]=@Time where [Номер профиля] = @Number", con);

                cmd.Parameters.AddWithValue("@Number", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtNameProfile", txtNameProfile.Text);
                cmd.Parameters.AddWithValue("@NumberPicket", txtNumberPicket.SelectedItem);
                cmd.Parameters.AddWithValue("@CoordinatesBeginning", txtCoordinatesBeginning.Text);
                cmd.Parameters.AddWithValue("@CoordinatesFracture", txtCoordinatesFracture.Text);
                cmd.Parameters.AddWithValue("@CoordinatesEnd", txtCoordinatesEnd.Text);
                cmd.Parameters.AddWithValue("@Length", txtLength.Text);
                cmd.Parameters.AddWithValue("@dateBen", dateBen.SelectedDate);
                cmd.Parameters.AddWithValue("@dateEnd", dateEnd.SelectedDate);
                cmd.Parameters.AddWithValue("@Time", Time.Content);

                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
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

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            NumberProfile tv = new NumberProfile();
            tv.ShowDialog();
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateProfile tv = new CreateProfile();
            tv.ShowDialog();
        }
    }
}
