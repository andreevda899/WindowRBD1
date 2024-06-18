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
using WindowRBD1.FormsCreate;
using WindowRBD1.Forms;

namespace WindowRBD1.FormsEdit
{
    public partial class EditContract : Window
    {
        public EditContract()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            Contract1();
        }

        public static bool flag = false;

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void Contract1() //Собирает [Номер договора] которые существуют в сущности Proekt.Договор на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер договора] FROM Proekt.Договор";
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

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            flag = true;

            if (flag == true)
            {
                Number.Text = "";
                flag = false;
            }

            Contract1();
        } 

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Договор SET [Наименование договора] = @txtNameContract, [Начала]  =@txtBeginnings,[Окончания] =@txtEndings, [Стоимость] =@txtCost,[Дата и время изменения записи] =@Time  where  [Номер договора] = @Number", con);
                cmd.Parameters.AddWithValue("@Number", Number.SelectedItem);
                cmd.Parameters.AddWithValue("@txtNameContract", txtNameContract.Text);
                cmd.Parameters.AddWithValue("@txtBeginnings", txtBeginnings.Text);
                cmd.Parameters.AddWithValue("@txtEndings", txtEndings.Text);
                cmd.Parameters.AddWithValue("@txtCost", txtCost.Text);
                cmd.Parameters.AddWithValue("@Time", Time.Content);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись изменена");
                con.Close();
            }
        }
        private void txtNameContract_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            if (flag == false) {
                string str = "Select * from Proekt.Договор where [Номер договора] = " + Number.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    txtNameContract.Text = dt.Rows[0][1].ToString();
                    txtBeginnings.Text = dt.Rows[0][2].ToString();
                    txtEndings.Text = dt.Rows[0][3].ToString();
                    txtCost.Text = dt.Rows[0][4].ToString();
                }
            }
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
           CreateContract rt = new CreateContract();
            rt.ShowDialog();
        }

        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            NumberContract rt = new NumberContract();
            rt.ShowDialog();
        }
    }
}
