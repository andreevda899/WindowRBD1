using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
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

namespace WindowRBD1.Forms
{
    public partial class FormLoading : Window
    {
        public FormLoading(string name, string FIO)
        {
            InitializeComponent();
            lineProgress();

            label1.Content += name;
            fio = FIO;
        }

        public static void checkTime() //секундомер
        {
            string time = string.Format("{0:mm:ss}", DateTime.Now);


            int minute = Int32.Parse(string.Format("{0:mm}", DateTime.Now));
            string sec = string.Format("{0:ss}", DateTime.Now);

            if (minute <= 20)
                minute++; 
            else
                minute = 01;

            string timer = minute + ":" + sec;

            if (minute < 10) 
                timer = "0" + minute + ":" + sec;


            while (true)
            {
                time = string.Format("{0:mm:ss}", DateTime.Now);
                if (timer.Equals(time))
                    Environment.Exit(0);

            }
        }

        string fio;
        SqlConnection con = new SqlConnection(BdCon.Con);

        private async void lineProgress() // Запуск ProgressBar
        {
            for (int i = 0; i <= 100; i++)
            {
                pgbEstatus.Value = i * (100 / 100);
                await Task.Delay(50);
                label2.Content = i + "%";
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Proekt.Пользователи  where ФИО = @Fio", con); // Определение роли пользователя для того чтобы дать доступ к базе данных
            cmd.Parameters.AddWithValue("@Fio", fio);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string usertype = dt.Rows[0][4].ToString();

                if (usertype == "Оператор")
                {
                    this.Hide();
                    MainWindowOperator red = new MainWindowOperator();
                    red.ShowDialog();
                    Thread thread = new Thread(checkTime);
                    thread.Start();
                    
                }

                if (usertype == "Супервайзер")
                {
                    this.Hide();
                    MainWindow red = new MainWindow();
                    red.ShowDialog();
                    Thread thread = new Thread(checkTime);
                    thread.Start();
                    label1.Margin = new Thickness(500, 470, 0, 0);

                }
                
            }

        }
    }
}
