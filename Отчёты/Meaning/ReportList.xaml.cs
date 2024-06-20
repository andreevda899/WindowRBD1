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
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.FormsMain;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace WindowRBD1.Отчёты
{
    /// <summary>
    /// Логика взаимодействия для ReportClient.xaml
    /// </summary>
    public partial class ReportList : Window
    {
        public ReportList()
        {
            InitializeComponent();
            txtList1();
        }
        string Number;

        private void txtList1() //При изменении значения происходит смена данных у всех элементов
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this, "Визитная карта");
            }
        }

        private void txtList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
