using DocumentFormat.OpenXml.Presentation;
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
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsCreate.Meaning;
using WindowRBD1.FormsCreate.Meaning.intermediateResult;
using static System.Windows.Forms.MonthCalendar;

namespace WindowRBD1.FormsEdit.Meaning
{
    public partial class EditIntermediateResult2 : Window
    {
        public EditIntermediateResult2()
        {
            InitializeComponent();
            Picket1();
            Number1();
        }
        public static bool flag = false;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "UPDATE Proekt.[Промежуточный результат 2] SET [Номер Пикета] = @txtNumberPicket,[Индекс Пикета] = @PicketIndex,[Значения измерения(ЭДС)] = @Meaning where [Номер Промежуточного результата 2] = @NumberTrans1 ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NumberTrans1", NumberTrans1.SelectedItem);
                cmd.Parameters.AddWithValue("@PicketIndex", PicketIndex.Text);
                cmd.Parameters.AddWithValue("@txtNumberPicket", txtNumberPicket.SelectedItem);
                cmd.Parameters.AddWithValue("@Meaning", Meaning.Text);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                conn.Close();
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            flag = true;

            if (flag == true)
            {
                NumberTrans1.Text = "";
                flag = false;
            }
            Number1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Picket1();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
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

        private void Number1()  //Собирает [Номер Промежуточного результата 1] которые существуют в сущности Proekt.Промежуточный результат 1 на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Промежуточного результата 2] FROM Proekt.[Промежуточный результат 2]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberTrans1.Items.Clear();
            while (reader.Read())
            {
                NumberTrans1.Items.Add(reader[0].ToString());
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
                dataAdapter.Fill(ds, "Proekt.Пикет");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Пикет"].DefaultView;
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreatePicket tv = new CreatePicket();
            tv.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            string sql = "select * from Proekt.[Промежуточный результат 2]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Промежуточный результат 2]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Промежуточный результат 2]"].DefaultView;
            }
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Просмотр данных 
        {
            CreateIntermediateResult2 tv = new CreateIntermediateResult2();
            tv.ShowDialog();
        }
        private void NumberTrans1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag == false) { 
                string str = "Select * from Proekt.[Промежуточный результат 2] where [Номер Промежуточного результата 2] = " + NumberTrans1.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    txtNumberPicket.Text = dt.Rows[0][1].ToString();
                    PicketIndex.Text = dt.Rows[0][2].ToString();
                    Meaning.Text = dt.Rows[0][3].ToString();


                    da.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
