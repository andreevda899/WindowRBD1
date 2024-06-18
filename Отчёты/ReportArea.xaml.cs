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
    public partial class ReportArea : Window
    {
        public ReportArea(string number)
        {
            InitializeComponent();
            Number = number;
            txtNameContract();
        }
        string Number;

        private void txtNameContract() //При изменении значения происходит смена данных у всех элементов
        {
            string str = "Select * from Proekt.Площади where [Номер площади] = " + Number;

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = str;
                SqlDataAdapter da = new SqlDataAdapter(myCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);

                txtArea.Content = dt.Rows[0][1].ToString();
                txtProfile.Content = dt.Rows[0][2].ToString();
                txtList.Content = dt.Rows[0][3].ToString();
                txtPerimeterLength.Content = dt.Rows[0][4].ToString();
                txtSizeArea.Content = dt.Rows[0][5].ToString();
                dateBeginning.Content = dt.Rows[0][6].ToString();
                dateEnd.Content = dt.Rows[0][7].ToString();
                txtSupervisorData.Content = dt.Rows[0][10].ToString();
                txtSupervisorOrder.Content = dt.Rows[0][11].ToString();

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
