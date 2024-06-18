using Org.BouncyCastle.Asn1.X509;
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
    public partial class ReportMethodology : Window
    {
        public ReportMethodology(string number)
        {
            InitializeComponent();
            Number = number;
            txtNameContract();
        }
        string Number;

        private void txtNameContract() //При изменении значения происходит смена данных у всех элементов
        {
            string str = "Select * from Proekt.Методика where [Номер методики] = " + Number;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                System.Data.DataTable dr = new System.Data.DataTable();
                da.Fill(dr);
                cmbNumber.Content = dr.Rows[0][0].ToString();
                cmbNameMethodology.Content = dr.Rows[0][1].ToString();
                txtGenerative.Content = dr.Rows[0][2].ToString();
                txtMeasuring.Content = dr.Rows[0][3].ToString();
                txtTelemetry.Content = dr.Rows[0][4].ToString();
                txtImpulse.Content = dr.Rows[0][5].ToString();
                txtPause.Content = dr.Rows[0][6].ToString();
                txtCurrent.Content = dr.Rows[0][7].ToString();

                da.Dispose();
                conn.Close();
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
