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
using WindowRBD1.Forms;
using WindowRBD1.FormsMain;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace WindowRBD1.Отчёты
{
    public partial class ReportOrder : Window
    {
        public ReportOrder(string number)
        {
            InitializeComponent();
            Number = number;
            txtNameContract();
        }
        string Number;

        private void txtNameContract() //При изменении значения происходит смена данных у всех элементов
        {
            string str = "Select * from Proekt.[Полевой отряд]  where [Номер отряда] = " + Number;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                System.Data.DataTable dr = new System.Data.DataTable();
                da.Fill(dr);

                NumberOrder.Content = dr.Rows[0][0].ToString();
                FioChief.Content = dr.Rows[0][1].ToString();
                FioITR.Content = dr.Rows[0][2].ToString();
                FioDriver.Content = dr.Rows[0][3].ToString();
                FioWorker.Content = dr.Rows[0][4].ToString();
                FioSupervisor.Content = dr.Rows[0][5].ToString();

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
