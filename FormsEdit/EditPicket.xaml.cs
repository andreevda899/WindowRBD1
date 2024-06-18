using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.Forms;
using WindowRBD1.FormsCreate;

namespace WindowRBD1.FormsEdit { 
    public partial class EditPicket : Window
    {
        public EditPicket()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            Picket1();
            Methodology1();
            Order1();
            Equipment1();
            Type1();
        }

        public static bool flag = false;

        private void Type1() // Заполнение данными в ComboBox
        {
            txtTypeMeasurement.ItemsSource = new string[] { "Рядовое", "Контрольное", "Опытное" };
        }

        private void Methodology1()  //Собирает [Номер методики] которые существуют в сущности Proekt.Методика на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер методики] FROM Proekt.Методика";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberMethodology.Items.Clear();
            while (reader.Read())
            {
                NumberMethodology.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Order1()  //Собирает [Номер отряда] которые существуют в сущности Proekt.[Полевой отряд] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер отряда] FROM Proekt.[Полевой отряд]";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberOrder.Items.Clear();
            while (reader.Read())
            {
                NumberOrder.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Picket1()  //Собирает [Номер Пикета] которые существуют в сущности Proekt.Пикет на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер Пикета] FROM Proekt.Пикет";
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

        private void Equipment1()  //Собирает [Номер оборудования] которые существуют в сущности Proekt.Оборудования на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер оборудования] FROM Proekt.Оборудования";
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            NumberEquipment.Items.Clear();
            while (reader.Read())
            {
                NumberEquipment.Items.Add(reader[0].ToString());
            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            int NumberOrder2 = Convert.ToInt32(NumberOrder.SelectedItem);
            int NumberMethodology2 = Convert.ToInt32(NumberMethodology.SelectedItem);
            int NumberEquipment2 = Convert.ToInt32(NumberEquipment.SelectedItem);

            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.Пикет SET [Наименование пикета] = @NamePicket,[Координата]=@Coordinate,[Вид измерения]=@TypeMeasurement,[Номер отряда]=@NumberOrder,[Номер методики]=@NumberMethodology,[Номер оборудования] =@NumberEquipment where [Номер Пикета] = @Number", con);
                
                cmd.Parameters.AddWithValue("@NamePicket", txtNamePicket.Text);
                cmd.Parameters.AddWithValue("@Coordinate", txtCoordinate);
                cmd.Parameters.AddWithValue("@TypeMeasurement", txtTypeMeasurement);
                cmd.Parameters.AddWithValue("@Number", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@NumberOrder", NumberOrder2);
                cmd.Parameters.AddWithValue("@NumberMethodology", NumberMethodology2);
                cmd.Parameters.AddWithValue("@NumberEquipment", NumberEquipment2);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            flag = true;

            if (flag == true)
            {
                txtNumber.Text = "";
                flag = false;
            }
            Picket1();
        }

        private void btRefresh1_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Order1();
        }

        private void btRefresh2_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Methodology1();
        }

        private void btRefresh3_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            Equipment1();
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btPoisk1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberMet met = new NumberMet();
            met.ShowDialog();
        }

        private void btPoisk2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberOrder met = new NumberOrder();
            met.ShowDialog();
        }
        private void btPoisk3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            NumberEq met = new NumberEq();
            met.ShowDialog();
        }

        private void txtNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag == false)
            {
                string str = "Select * from Proekt.Пикет  where [Номер Пикета] = " + txtNumber.SelectedItem;

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    System.Data.DataTable dr = new System.Data.DataTable();
                    da.Fill(dr);

                    txtNamePicket.Text = dr.Rows[0][1].ToString();
                    txtCoordinate.Text = dr.Rows[0][2].ToString();
                    txtTypeMeasurement.Text = dr.Rows[0][3].ToString();
                    NumberOrder.Text = dr.Rows[0][4].ToString();
                    NumberMethodology.Text = dr.Rows[0][5].ToString();
                    NumberEquipment.Text = dr.Rows[0][6].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }

        private void btAdd1_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateMethodology tv = new CreateMethodology();
            tv.ShowDialog();
        }

        private void btAdd2_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateOrder tv = new CreateOrder();
            tv.ShowDialog();
        }

        private void btAdd3_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreateEquipment tv = new CreateEquipment();
            tv.ShowDialog();
        }

        private void btAdd4_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            CreatePicket tv = new CreatePicket();
            tv.ShowDialog();
        }

        private void btPoisk4_Click(object sender, RoutedEventArgs e) //Открытие формы
        {
            
        }
    }
}
