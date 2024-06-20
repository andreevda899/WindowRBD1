using Microsoft.Office.Interop.Excel;
using Spire.Xls.AI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using WindowRBD1.FormsMain;
using WindowRBD1.Отчёты;

namespace WindowRBD1.Forms
{
    public partial class NumberList : System.Windows.Window
    {
        public NumberList()
        {
            InitializeComponent();
            dataGridView();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        int uri = 27;
        string sql;

        private void Button_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Close();
        }

        private void dataGridView() //Происходит заполнение данными datagrid
        {
            string sql = "select * from Proekt.[Список координат углов периметра]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, "Proekt.[Список координат углов периметра]");
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Список координат углов периметра]"].DefaultView;
            }
        }

        private void btExport_Click(object sender, RoutedEventArgs e) // Экспорт данных из datagid в Excel
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add("C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\База Данных.xlsx");
            Worksheet sheet1 = (Worksheet)workbook.Sheets[uri];

            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 50;
                myRange.Value2 = dataGridView1.Columns[j].Header;
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Items.Count; j++)
                {
                    TextBlock b = dataGridView1.Columns[i].GetCellContent(dataGridView1.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void ExportToTxt_Click(object sender, RoutedEventArgs e) // Экспорт данных из datagid в txt
        {
            string path = "C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\ExportToTxt\\exportArea.txt";
            StreamWriter sw = new StreamWriter(path);
            System.Data.DataTable dt = Select1("Select * from Proekt.[Список координат углов периметра]");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("[Номер списка углов периметра]:" + dt.Rows[i][0].ToString());
                sw.WriteLine("[x1]:" + dt.Rows[i][1].ToString());
                sw.WriteLine("[y1]:" + dt.Rows[i][2].ToString());
                sw.WriteLine("[x2]:" + dt.Rows[i][3].ToString());
                sw.WriteLine("[y2]:" + dt.Rows[i][4].ToString());
                sw.WriteLine("[x3]:" + dt.Rows[i][5].ToString());
                sw.WriteLine("[y3]:" + dt.Rows[i][6].ToString());
                sw.WriteLine("");
            }
            sw.Close();
            Process.Start("notepad.exe", path);
        }

        public System.Data.DataTable Select1(string selectSql) // Подсоединение к базе данных для экспорта в txt
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection sqlConnection = new SqlConnection(BdCon.Con);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSql;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dt);
            return dt;
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) // Обновление данными datagrid
        {
            ds.Clear();
            sql = "select * from Proekt.[Список координат углов периметра]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Список координат углов периметра]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Список координат углов периметра]"].DefaultView;
            }
        }

        private void btReport_Click(object sender, RoutedEventArgs e) // Создание отчета
        {
            new ReportList().ShowDialog();
        }
    }
}
