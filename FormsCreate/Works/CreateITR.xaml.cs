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
    public partial class CreateITR : Window
    {
        public CreateITR()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для отправки введённых данных на Sql Server
        {
            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                string sql = "insert into Proekt.ИТР([ФИО],[Квалификация],[Опыт работы в коллективе],[Общий опыт работы по специальности],[Дата прохождения медосмотра])" +
                $"values ('{txtFIO.Text}','{txtCompetence.Text}','{txtExperience.Text}','{txtExperienced.Text}','{dateMedical.SelectedDate}')";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.ExecuteNonQuery();
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
