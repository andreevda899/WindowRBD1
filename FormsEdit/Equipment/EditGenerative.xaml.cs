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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.FormsCreate.Equipment;

namespace WindowRBD1.FormsEdit.Equipment
{
    public partial class EditGenerative : Window
    {
        public EditGenerative()
        {
            InitializeComponent();

	//Создание часов для добавления записи [Дата и время появления записи]
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToString(); };
            timer.Start();
	//
            txtName1();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            txtName1();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void txtName1() //Собирает [Наименование] которые существуют в сущности Proekt.[Генеративное оборудование] на Sql Server
        {
            SqlConnection connection = new SqlConnection(BdCon.Con);
            string sql = "SELECT [Номер генеративного оборудования] FROM Proekt.[Генеративное оборудование]";
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

        private void txtName_SelectionChanged(object sender, SelectionChangedEventArgs e) //При изменении значения происходит смена данных у всех элементов
        {
            //int txtName2 = Convert.ToInt32(txtName.SelectedItem);

            string str = "Select * from Proekt.[Генеративное оборудование]  where [Номер генеративного оборудования] = " + txtNumber.SelectedItem;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                System.Data.DataTable dr = new System.Data.DataTable();
                da.Fill(dr);
                txtName.Text = dr.Rows[0][1].ToString();
                txtInventory.Text = dr.Rows[0][2].ToString();
                dateVerification.Text = dr.Rows[0][3].ToString();
                datePurchases.Text = dr.Rows[0][4].ToString();
                txtCharacteristic.Text = dr.Rows[0][5].ToString();

                da.Dispose();
                conn.Close();
            }
        }
        private void btEdit_Click(object sender, RoutedEventArgs e) //Подключение к базе дынных для изменения введённых данных на Sql Server
        {
            //int cmbNumberEquipment2 = Convert.ToInt32(txtName.SelectedItem);

            using (SqlConnection con = new SqlConnection(BdCon.Con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Proekt.[Генеративное оборудование] SET [Наименование] = @txtName,[Инвентарный номер] = @Inventory,[Дата приобретения]=@dateVerification,[Дата поверки]=@datePurchases, [Характеристики]  = @Characteristic where [Номер генеративного оборудования] = @txtNumber", con);
                cmd.Parameters.AddWithValue("@txtNumber", txtNumber.SelectedItem);
                cmd.Parameters.AddWithValue("@txtName", txtName.Text);
                cmd.Parameters.AddWithValue("@Inventory", txtInventory.Text);
                cmd.Parameters.AddWithValue("@Characteristic", txtCharacteristic.Text);
                cmd.Parameters.AddWithValue("@dateVerification", dateVerification.SelectedDate);
                cmd.Parameters.AddWithValue("@datePurchases", datePurchases.SelectedDate);
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Запись изменена");
                con.Close();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) //закрытие формы
        {
            this.Close();
        }

        private void btPoisk_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
            string sql = "select [Номер генеративного оборудования],[Наименование] from Proekt.[Генеративное оборудование]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.[Генеративное оборудование]");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Генеративное оборудование]"].DefaultView;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e) //Просмотр данных в datagidview
        {
           CreateGenerative tv = new CreateGenerative();
            tv.ShowDialog();
        }

    }
}
