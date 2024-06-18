using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Configuration;
using System.Data.SqlTypes;
using Moq;
using Microsoft.Office.Interop.Excel;
using WindowRBD1.Forms;
using WindowRBD1.FormsCreate;

namespace WindowRBD1.FormsEdit
{
    public partial class EditMethodology : System.Windows.Window
    {
        public EditMethodology()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время изменения записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            ListCategories();
            ListCategories1();
            ListCategories2();
        }

        public static bool flag = false;
        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            int test1 = Convert.ToInt32(txtGenerative.SelectedItem);
            int test2 = Convert.ToInt32(txtMeasuring.SelectedItem);
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Методика SET  [Наименование методики] = @txtNameMethodology,[Номер описания ген.установки]= @txtGenerative, [Номер описания измер.установки] = @txtMeasuring, [Описание телеметрической установки]  = @txtTelemetry,[Продолжительность импульса] = @txtImpulse, [Продолжительность паузы] = @txtPause,[Сила тока] = @txtCurrent where [Номер методики] = @txtNumber", con);
                cmd.Parameters.AddWithValue("@txtNumber", cmbNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtNameMethodology", cmbNameMethodology.Text);
                cmd.Parameters.AddWithValue("@txtGenerative", test1);
                cmd.Parameters.AddWithValue("@txtMeasuring", test2);
                cmd.Parameters.AddWithValue("@txtTelemetry", txtTelemetry.Text);
                cmd.Parameters.AddWithValue("@txtImpulse", txtImpulse.Text);
                cmd.Parameters.AddWithValue("@txtPause", txtPause.Text);
                cmd.Parameters.AddWithValue("@txtCurrent", txtCurrent.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            flag = true;

            if (flag == true)
            {
                cmbNumber.Text = "";
                flag = false;
            }

            ListCategories();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            ListCategories1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            ListCategories2();
        }

        private void ListCategories()//Собирает [Номер методики] которые существуют в сущности Proekt.Методика на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);

            string sql = "SELECT [Номер методики] FROM Proekt.Методика";

            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            cmbNumber.Items.Clear();

            while (reader.Read())
            {
                cmbNumber.Items.Add(reader[0].ToString());
               
            }
            reader.Close();

            command.Dispose();
            connection.Close();
        }

        private void ListCategories1() //Собирает [Номер описания ген.установки] которые существуют в сущности Proekt.[Описание генераторной установки] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);

            string sql = "SELECT [Номер описания ген.установки] FROM Proekt.[Описание генераторной установки]";

            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            txtGenerative.Items.Clear();

            while (reader.Read())
            {
                txtGenerative.Items.Add(reader[0].ToString());
                
            }
            reader.Close();

            command.Dispose();
            connection.Close();
        }

        private void ListCategories2() //Собирает [Номер описания измер.установки] которые существуют в сущности Proekt.[Описание измерительной установки] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);

            string sql = "SELECT [Номер описания измер.установки] FROM Proekt.[Описание измерительной установки]";

            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            txtMeasuring.Items.Clear();

            while (reader.Read())
            {
                txtMeasuring.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command.Dispose();
            connection.Close();
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberADDGen tr = new NumberADDGen();
            tr.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberADDMeauring tr = new NumberADDMeauring();
            tr.ShowDialog();
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberGenerative tv = new NumberGenerative();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberMeasuring tv = new NumberMeasuring();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateMethodology tr = new CreateMethodology();
            tr.ShowDialog();
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberMethodology tv = new NumberMethodology();
            tv.ShowDialog();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void cmbNameMethodology_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag == false) { 
                string str = "Select * from Proekt.Методика where [Номер методики] = " + cmbNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);

                    cmbNameMethodology.Text = dr.Rows[0][1].ToString();
                    txtGenerative.Text = dr.Rows[0][2].ToString();
                    txtMeasuring.Text = dr.Rows[0][3].ToString();
                    txtTelemetry.Text = dr.Rows[0][4].ToString();
                    txtImpulse.Text = dr.Rows[0][5].ToString();
                    txtPause.Text = dr.Rows[0][6].ToString();
                    txtCurrent.Text = dr.Rows[0][7].ToString();

                    da.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
