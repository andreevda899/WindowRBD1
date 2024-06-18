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
using System.Security.Principal;
using WindowRBD1.Forms;

namespace WindowRBD1.FormsEdit
{
    public partial class EditClient : Window
    {
        public EditClient()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            Client1();
        }

        private void Client1() //Собирает [Номер заказчика] которые существуют в сущности Proekt.Заказчик на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер заказчика] FROM Proekt.Заказчик";
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

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Заказчик SET [Название компаний] = @txtNameCompany,[Юридический адрес]  =@txtLegalAddress,[Фактический адрес] =@txtActualAddress, [ИНН] =@txtINN,[КПК] = @txtPda, [Расчетный счёт]  =@txtCalculated,[Корреспондентский счёт] =@txtCorrespondent, [Представитель] =@txtAgent,[Телефон представителя] = @txtPhone, [Адрес электронной почты]  =@txtEmail,[Сайт] =@txtSite, [Дата и время изменения записи] =@Time  where [Номер заказчика] = @txtNumber", con);
                cmd.Parameters.AddWithValue("@txtNumber", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtNameCompany", txtNameCompany.Text);
                cmd.Parameters.AddWithValue("@txtLegalAddress", txtLegalAddress.Text);
                cmd.Parameters.AddWithValue("@txtActualAddress", txtActualAddress.Text);
                cmd.Parameters.AddWithValue("@txtINN", txtINN.Text);
                cmd.Parameters.AddWithValue("@txtPda", txtPda.Text);
                cmd.Parameters.AddWithValue("@txtCalculated", txtCalculated.Text);
                cmd.Parameters.AddWithValue("@txtCorrespondent", txtCorrespondent.Text);
                cmd.Parameters.AddWithValue("@txtAgent", txtAgent.Text);
                cmd.Parameters.AddWithValue("@txtPhone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@txtEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@txtSite", txtSite.Text);
                cmd.Parameters.AddWithValue("@Time", Time.Content);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btPoisk_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            NumberClient rt = new NumberClient();
            rt.ShowDialog();
        }

        private void txtNameCompany_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            string str = "Select * from Proekt.Заказчик where [Номер заказчика] = " + txtNumber.SelectedItem;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtNameCompany.Text = dt.Rows[0][1].ToString();
                txtLegalAddress.Text = dt.Rows[0][2].ToString();
                txtActualAddress.Text = dt.Rows[0][3].ToString();
                txtINN.Text = dt.Rows[0][4].ToString();
                txtPda.Text = dt.Rows[0][5].ToString();
                txtCalculated.Text = dt.Rows[0][6].ToString();
                txtCorrespondent.Text = dt.Rows[0][7].ToString();
                txtAgent.Text = dt.Rows[0][8].ToString();
                txtPhone.Text = dt.Rows[0][9].ToString();
                txtEmail.Text = dt.Rows[0][10].ToString();
                txtSite.Text = dt.Rows[0][11].ToString();

                da.Dispose();
                conn.Close();
            }
        }
    }
}
