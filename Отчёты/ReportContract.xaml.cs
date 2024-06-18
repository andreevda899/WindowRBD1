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
    public partial class ReportContract : Window
    {
        public ReportContract(string number)
        {
            InitializeComponent();
            Number = number;
            txtNameContract1();
        }
        string Number;

        private void txtNameContract1() //При изменении значения происходит смена данных у всех элементов
        {
            string str = "Select * from Proekt.Договор where [Номер договора] = " + Number;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Number1.Content = dt.Rows[0][0].ToString();
                txtNameContract.Content = dt.Rows[0][1].ToString();
                txtBeginnings.Content = dt.Rows[0][2].ToString();
                txtEndings.Content = dt.Rows[0][3].ToString();
                txtCost.Content = dt.Rows[0][4].ToString();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(this, "Визитная карта");
            }
        }
    }
}
