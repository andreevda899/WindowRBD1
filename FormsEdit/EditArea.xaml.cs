using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsCreate.Meaning;
using WindowRBD1.FormsEdit.Works;
using static System.Windows.Forms.MonthCalendar;

namespace WindowRBD1.FormsEdit
{
    public partial class EditArea : System.Windows.Window
    {
        public EditArea()
        {
            InitializeComponent();
            
	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
            //
            txtList1();
            txtArea1();
            txtProfile1();
            txtSupervisorOrder1();
            txtSupervisorData1();
        }

        public static bool flag = false;

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

        private void btRefresh4_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            flag = true;

            if (flag == true)
            {
                txtNumber.Text = "";
                flag = false;
            }

            txtArea1();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        { 
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Площади SET [Наименование площади] = @txtArea,[Номер профиля] = @txtProfile,[Номер списка углов периметра] =@txtList, [Длина периметра] =@txtPerimeterLength, [Величина площади окружённая периметром] =@txtSizeArea,[Дата и время начала работ] =@dateBeginning,[Дата и время окончания работ] =@dateEnd, [Дата и время изменения записи] =@Time, [Супервайзер полевых работ] =@txtSupervisorOrder, [Супервайзер обработки данных] =@txtSupervisorData where [Номер площади] = @txtNumber", con);
                cmd.Parameters.AddWithValue("@txtNumber", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtArea", txtArea.Text);
		        cmd.Parameters.AddWithValue("@txtProfile", txtProfile.Text);
                cmd.Parameters.AddWithValue("@txtList", txtList.SelectedItem);
                cmd.Parameters.AddWithValue("@txtSizeArea", txtSizeArea.Text);
                cmd.Parameters.AddWithValue("@txtPerimeterLength", txtPerimeterLength.Text);
                cmd.Parameters.AddWithValue("@txtSupervisorData", txtSupervisorData.Text);
                cmd.Parameters.AddWithValue("@txtSupervisorOrder", txtSupervisorOrder.Text);
                cmd.Parameters.AddWithValue("@dateBeginning", dateBeginning.SelectedDate);
                cmd.Parameters.AddWithValue("@dateEnd", dateEnd.SelectedDate);
                cmd.Parameters.AddWithValue("@Time", Time.Content);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        } 
      
        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void txtArea1() //Собирает [Номер площади] которые существуют в сущности Proekt.Площади на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер площади] FROM Proekt.Площади";
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

        private void txtProfile1() //Собирает [Номер площади] которые существуют в сущности Proekt.Площади на Sql Server
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

        private void txtNumber_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if(flag == false) { 
                string str = "Select * from Proekt.Площади where [Номер площади] = " + txtNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    txtArea.Text = dt.Rows[0][1].ToString();
                    txtProfile.Text = dt.Rows[0][2].ToString();
                    txtList.Text = dt.Rows[0][3].ToString();
                    txtPerimeterLength.Text = dt.Rows[0][4].ToString();
                    txtSizeArea.Text = dt.Rows[0][5].ToString();
                    dateBeginning.Text = dt.Rows[0][6].ToString();
                    dateEnd.Text = dt.Rows[0][7].ToString();
                    txtSupervisorData.Text = dt.Rows[0][10].ToString();
                    txtSupervisorOrder.Text = dt.Rows[0][11].ToString();

                    da.Dispose();
                    conn.Close();
                }
            }
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

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberArea tv = new NumberArea();
            tv.ShowDialog();
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateArea tv = new CreateArea();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateList tv = new CreateList();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberList tv = new NumberList();
            tv.ShowDialog();
        }

        private void btEdit1_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            EditList rt = new EditList();
            rt.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            EditSupervisor tv = new EditSupervisor();
            tv.ShowDialog();
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberSupervisor tv = new NumberSupervisor();
            tv.ShowDialog();
        }
    }
}
