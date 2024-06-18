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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.Forms;
namespace WindowRBD1.Forms
{
    public partial class NumberContract : Window
    {
        public NumberContract()
        {
            InitializeComponent();
            dataGridView();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void Button_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Close();
        }

        private void dataGridView() //Происходит заполнение данными datagrid
        {
            string sql = "select * from Proekt.Договор";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.Договор");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Договор"].DefaultView;
            }
        }
    }
}
