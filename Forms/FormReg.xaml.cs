using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
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
using WindowRBD1.FormsEdit;
using static System.Net.Mime.MediaTypeNames;

namespace WindowRBD1.Forms
{
    public partial class FormReg : Window
    {
        public FormReg()
        {
            InitializeComponent();
            res.Source = new System.Windows.Media.ImageSourceConverter().ConvertFromString("C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\Image\\iconsClosePassword.png") as System.Windows.Media.ImageSource;

            Display.AddHandler(System.Windows.Controls.Button.PreviewMouseDownEvent, new MouseButtonEventHandler(Btn_PreviewMouseDown));
            Display.AddHandler(System.Windows.Controls.Button.PreviewMouseUpEvent, new MouseButtonEventHandler(Btn_PreviewMouseUp));

            //Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
            //
        }

        SqlConnection con = new SqlConnection(BdCon.Con);
        public string FIO;

        private void BtnEntry_Click(object sender, RoutedEventArgs e) //Происходит проверка логина и пароля введенные пользователем в бд 
        {
            if (txtLog.Text != "" && pwdPasswordBox.Password != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Proekt.Пользователи  where Логин = @login and Пароль = @password", con);
                cmd.Parameters.AddWithValue("@login", txtLog.Text);
                cmd.Parameters.AddWithValue("@password", pwdPasswordBox.Password);
                SqlCommand cmd2 = new SqlCommand("UPDATE Proekt.Пользователи SET [Дата и время последнего входа] = @Time  where Логин = @login and Пароль = @password", con);
                cmd2.Parameters.AddWithValue("@Time", Time.Content);
                cmd2.Parameters.AddWithValue("@login", txtLog.Text);
                cmd2.Parameters.AddWithValue("@password", pwdPasswordBox.Password);
                cmd2.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string usertype = dt.Rows[0][4].ToString();
                    if (usertype == "Оператор")
                    {
                        SqlCommand cmd1 = new SqlCommand("select ФИО from  Proekt.Пользователи where Логин = @login", con);
                        cmd1.Parameters.AddWithValue("@login", txtLog.Text);
                        FIO = (string)cmd1.ExecuteScalar();
                        new FormLoading(FIO + ", Добро пожаловать в окно Оператора!",FIO).ShowDialog();
                    }
                    if (usertype == "Супервайзер")
                    {
                        SqlCommand cmd1 = new SqlCommand("select ФИО from  Proekt.Пользователи where Логин = @login", con);
                        cmd1.Parameters.AddWithValue("@login", txtLog.Text);
                        FIO = (string)cmd1.ExecuteScalar();
                        new FormLoading(FIO + ", Добро пожаловать в окно Супервайзера!", FIO).ShowDialog();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Такого пользователя нету!", "Успешно!", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                }
                con.Close();
            }
        }

        private void BtnPassword_Click(object sender, RoutedEventArgs e) // открытие формы  
        {
            ForgotPassword td = new ForgotPassword();
            td.ShowDialog();
        }
        
        private void Btn_PreviewMouseDown(object sender, MouseButtonEventArgs e) //меняет местами Password и TextBox
        {
            res.Source = new System.Windows.Media.ImageSourceConverter().ConvertFromString("C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\Image\\iconsDisplayPassword.png") as System.Windows.Media.ImageSource;

            pwdTextBox.Text = pwdPasswordBox.Password; // скопируем в TextBox из PasswordBox
            pwdTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
            pwdPasswordBox.Visibility = Visibility.Hidden; // PasswordBox - скрыть
        }

        private void Btn_PreviewMouseUp(object sender, MouseButtonEventArgs e) //меняет местами TextBox и Password  
        {
            res.Source = new System.Windows.Media.ImageSourceConverter().ConvertFromString("C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\Image\\iconsClosePassword.png") as System.Windows.Media.ImageSource;

            pwdPasswordBox.Password = pwdTextBox.Text; // скопируем в PasswordBox из TextBox 
            pwdTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
            pwdPasswordBox.Visibility = Visibility.Visible; // PasswordBox - отобразить
        }

    }
}
