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

namespace WindowRBD1.FormsCreate.Works
{
    public partial class CreateChief : Window
    {
        public CreateChief()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void btcreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "INSERT INTO Proekt.[Начальник отряда]([ФИО],[Квалификация],[Опыт работы в коллективе],[Общий опыт работы по специальности],[Дата прохождения медосмотра]) VALUES(@txtFIO, @txtCompetence, @txtExperience, @txtExperienced, @dateMedical)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@txtFIO", txtFIO.Text);
                cmd.Parameters.AddWithValue("@txtCompetence", txtCompetence.Text);
                cmd.Parameters.AddWithValue("@txtExperience", txtExperience.Text);
                cmd.Parameters.AddWithValue("@txtExperienced", txtExperienced.Text);
                cmd.Parameters.AddWithValue("@dateMedical", dateMedical.SelectedDate);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись добавлена");
                conn.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }
    }
}
