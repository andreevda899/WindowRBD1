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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.Forms;

namespace WindowRBD1.FormsCreate.Meaning.intermediateResult
{
    public partial class CreateIntermediateResult1 : Window
    {
        public CreateIntermediateResult1()
        {
            InitializeComponent();
            Picket1();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void btcreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "INSERT INTO Proekt.[Промежуточный результат 1]([Номер Пикета],[Индекс Пикета],[Значения измерения(ЭДС)]) VALUES(@txtNumberPicket,@PicketIndex, @Meaning)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PicketIndex", PicketIndex.Text);
                cmd.Parameters.AddWithValue("@txtNumberPicket", txtNumberPicket.SelectedItem);
                cmd.Parameters.AddWithValue("@Meaning", Meaning.Text);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            Picket1();
        }

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

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            string sql = "select * from Proekt.Пикет";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.Пикет");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.Пикет"].DefaultView;
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreatePicket tv = new CreatePicket();
            tv.ShowDialog();
        }
    }
}
