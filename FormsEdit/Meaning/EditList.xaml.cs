using System;
using System.Collections.Generic;
using System.Data;
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

namespace WindowRBD1.FormsCreate.Meaning
{
    public partial class EditList : Window
    {
        public EditList()
        {
            InitializeComponent();
            List1();
        }

        public static bool flag = false;

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "UPDATE Proekt.[Список координат углов периметра] SET [x1] = @txtX1,[y1] = @txtY1,[x2] = @txtX2,[y2] = @txtY2,[x3] = @txtX3,[y3] = @txtY3 where [Номер списка углов периметра] = @txtList";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txtList", txtList.SelectedItem);
                cmd.Parameters.AddWithValue("@txtX1", txtX1.Text);
                cmd.Parameters.AddWithValue("@txtY1", txtY1.Text);
                cmd.Parameters.AddWithValue("@txtX2", txtX2.Text);
                cmd.Parameters.AddWithValue("@txtY2", txtY2.Text);
                cmd.Parameters.AddWithValue("@txtX3", txtX3.Text);
                cmd.Parameters.AddWithValue("@txtY3", txtY3.Text);
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
                txtList.Text = "";
                flag = false;
            }
            List1();
        }

        private void List1()  //Собирает [Номер списка углов периметра] которые существуют в сущности Proekt.[Список координат углов периметра] на Sql Server
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

        private void txtList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag == false) { 
                string str = "Select * from Proekt.[Список координат углов периметра] where [Номер списка углов периметра] = " + txtList.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    txtX1.Text = dt.Rows[0][1].ToString();
                    txtY1.Text = dt.Rows[0][2].ToString();
                    txtX2.Text = dt.Rows[0][3].ToString();
                    txtY2.Text = dt.Rows[0][4].ToString();
                    txtX3.Text = dt.Rows[0][5].ToString();
                    txtY3.Text = dt.Rows[0][6].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }
    }
}
