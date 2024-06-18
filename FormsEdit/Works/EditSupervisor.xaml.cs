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
using WindowRBD1.FormsCreate.Works;

namespace WindowRBD1.FormsEdit.Works
{
    public partial class EditSupervisor : Window
    {
        public EditSupervisor()
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
        }

        public static bool flag = false;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

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

        private void txtName1() //Собирает [Номер Супервайзера] которые существуют в сущности Proekt.[Супервайзер] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Супервайзера] FROM Proekt.[Супервайзер]";
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

        private void btEdit_Click(object sender, RoutedEventArgs e)//Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.[Супервайзер] SET ФИО = @txtFIO,[Квалификация]  = @Competence, [Опыт работы в коллективе]  = @Experience, [Общий опыт работы по специальности] = @Experienced where [Номер Супервайзера] = @txtNumber", con);
                cmd.Parameters.AddWithValue("@txtNumber", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtFIO", txtFIO.Text);
                cmd.Parameters.AddWithValue("@Competence", txtCompetence.Text);
                cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                cmd.Parameters.AddWithValue("@Experienced", txtExperienced.Text);
                //cmd.Parameters.AddWithValue("@NumberTelemetryEquipment", cmbNumberTelemetryEquipment2);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void txtFIO_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false)
            {
                string str = "Select * from Proekt.[Супервайзер]  where [Номер Супервайзера] = " + txtNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);
                    txtFIO.Text = dr.Rows[0][1].ToString();
                    txtCompetence.Text = dr.Rows[0][2].ToString();
                    txtExperience.Text = dr.Rows[0][3].ToString();
                    txtExperienced.Text = dr.Rows[0][4].ToString();
                    dateMedical.Text = dr.Rows[0][5].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateSupervisor tv = new CreateSupervisor();
            tv.ShowDialog();
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select * from Proekt.Супервайзер";

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
    }
}
