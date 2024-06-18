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

namespace WindowRBD1.Forms
{
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
            Role1();
        }

        SqlConnection con = new SqlConnection(BdCon.Con);

        private void Role1() // Получение роли пользователя
        {
            cmbRole.ItemsSource = new string[] { "Оператор", "Супервайзер" };
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) // закрытие формы
        {
            this.Close();
        }

        private void BtnProv_Click(object sender, RoutedEventArgs e) // Проверка пользователя в базе данных 
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Пароль from Proekt.Пользователи where Логин = @login and Роль = @Role", con);
            cmd.Parameters.AddWithValue("@login", txtLogin.Text);
            cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                txtPassword.Text = dr[0].ToString();
            }

            else
            {
                MessageBox.Show("username not available");
                txtPassword.Text = "";
            }

            con.Close();
        }
    }
}
