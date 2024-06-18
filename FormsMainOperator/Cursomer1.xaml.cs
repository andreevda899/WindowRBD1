using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WindowRBD1.Class;
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsCreate.Equipment;
using WindowRBD1.FormsCreate.Works;
using WindowRBD1.FormsEdit;
using WindowRBD1.FormsEdit.Equipment;
using WindowRBD1.FormsEdit.Works;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

using Range = Microsoft.Office.Interop.Excel.Range;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowRBD1.FormsMain;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Policy;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using WindowRBD1.Отчёты;
using WindowRBD1.Forms;
using DocumentFormat.OpenXml.Presentation;
using static System.Windows.Forms.MonthCalendar;

namespace WindowRBD1.FormsMainOperator
{
    public partial class Cursomer1 : System.Windows.Window
    {
        public Cursomer1()
        {
            InitializeComponent();
            CursomerCommand();
        }

        string con = BdCon.Con;
        public string RoleBox;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable dtSales = new DataTable();
        string sql;
        public string str3;
        int uri;

        public static bool flag = false;

        private void СlearCommand_Click(object sender, RoutedEventArgs e) 
        {
            flag = true;

            dataGridView1.UnselectAll();

            if (flag == true)
            {
                NumberClient.Content = "";
                txtNameCompany.Content = "";
                txtLegalAddress.Text = "";
                txtActualAddress.Text = "";
                txtINN.Content = "";
                txtPda.Content = "";
                txtCalculated.Content = "";
                txtCorrespondent.Content =  "";
                txtAgent.Text = "";
                txtPhone.Content = "";
                Email.Text = "";
                Site.Text = "";
                DateCreate.Text = "";
                DateEdit.Text = "";
                flag = false;
            }
        }

        private void CursomerCommand() //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.Заказчик";
            cmbTab.ItemsSource = new string[] { "Номер заказчика", "Название компаний", "Представитель" };
            RoleBox = "Proekt.Заказчик";
            uri = 1;
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;

                dataAdapter.Fill(ds, "Proekt.Заказчик");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Заказчик"].DefaultView;
            }
        }

        private void MeaningCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Meaning1").ShowDialog();
        }

        private void ProjectCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Project1").ShowDialog();
        }

        private void ContractCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Contract1").ShowDialog();
        }

        private void CmTab1()
        {
            if (cmbTab.Text == "Номер заказчика")
            {
                str3 = "[Номер заказчика]";
            }

            if (cmbTab.Text == "Название компаний")
            {
                str3 = "[Название компаний]";
            }

            if (cmbTab.Text == "Представитель")
            {
                str3 = "[Представитель]";
            }
        }

        private void Button_Click(object sender, EventArgs e, string sql) //Поисковик
        {
            CmTab1();
            if (txtBox1.Text == "") { sql = "select * from " + RoleBox; }
            else
            {
                sql = "select * from " + RoleBox + " where " + str3 + "= '" + txtBox1.Text + "'";
            }

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                dataAdapter.SelectCommand = myCommand;
                // Заполняем ds данными из dataAdapter:
                dataAdapter.Fill(ds, str3);
                // Указываем источник данных DataSource для dataGrid1: 
                dataGridView1.ItemsSource = ds.Tables[str3].DefaultView;
            } // end using

        }

        private void Button_Click(object sender, EventArgs e) //Вызов Поисковика
        {
            Button_Click(sender, e, sql);
        }

        private void MeasuringCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Measuring1").ShowDialog();
        }

        private void GenerativeCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Generative1").ShowDialog();
        }

        private void TelemetryCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Telemetry1").ShowDialog();
        }

        private void PicketCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Picket1").ShowDialog();
        }

        private void AreaCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Area1").ShowDialog();
        }

        private void ProfileCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Profile1").ShowDialog();
        }

        private void AddCommand_Click(object sender, RoutedEventArgs e) //Происходит открытие формы
        {
            CreateClient form1 = new CreateClient();
            form1.Show();
        }

        private void EditCommand_Click(object sender, RoutedEventArgs e) //Происходит открытие формы
        {
            EditClient form1 = new EditClient();
            form1.Show();
        }

        private void DeleteCommand_Click(object sender, RoutedEventArgs e) //Происходит удаление записи из базы данных
        {
 		    if (txtBox1.Text != "")
  		    {
      			using (SqlConnection con = new SqlConnection(BdCon.Con))
      			{
          			con.Open();
          			DataRowView rowView = dataGridView1.SelectedValue as DataRowView;
          			SqlCommand cmd = new SqlCommand("Delete from " + RoleBox + " where " + str3 + " = '" + txtBox1.Text + "'", con);
          			cmd.ExecuteNonQuery();
          			System.Windows.MessageBox.Show("Запись удалена");
          			con.Close();
     			}
	
      			sql = "select * from " + RoleBox;

      			using (SqlConnection conn = new SqlConnection(con))
      			{
         			conn.Open();
          			SqlCommand myCommand = new SqlCommand();
          			myCommand.Connection = conn;
          			myCommand.CommandText = sql;

          			ds.Clear();
          			dataAdapter.SelectCommand = myCommand;
          			// Заполняем ds данными из dataAdapter:
          			dataAdapter.Fill(ds, RoleBox);
          			// Указываем источник данных DataSource для dataGrid1: 
          			dataGridView1.ItemsSource = ds.Tables[RoleBox].DefaultView;
      			} // end using
  		    }
  		    else
  		    {
                System.Windows.MessageBox.Show("Введите в поисковик Номер заказчика для удаления");
  		    }
        }

        private void btClose_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Close();
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e) // Обновление данными datagrid
        {
            ds.Clear();
            sql = "select * from " + RoleBox;

            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;
                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, RoleBox);
                dataGridView1.ItemsSource = ds.Tables[RoleBox].DefaultView;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) //Происходит выделение строки и заполнение информацией элементов
        {
            dataGridView1.IsReadOnly = true;

            if (flag == false) { 
                DataRowView rowView = dataGridView1.SelectedValue as DataRowView;

                string str = "Select * from Proekt.Заказчик where [Номер заказчика] = " + rowView[0].ToString();

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    NumberClient.Content = dt.Rows[0][0].ToString();
                    txtNameCompany.Content = dt.Rows[0][1].ToString();
                    txtLegalAddress.Text = dt.Rows[0][2].ToString();
                    txtActualAddress.Text = dt.Rows[0][3].ToString();
                    txtINN.Content = dt.Rows[0][4].ToString();
                    txtPda.Content = dt.Rows[0][5].ToString();
                    txtCalculated.Content = dt.Rows[0][6].ToString();
                    txtCorrespondent.Content = dt.Rows[0][7].ToString();
                    txtAgent.Text = dt.Rows[0][8].ToString();
                    txtPhone.Content = dt.Rows[0][9].ToString();
                    Email.Text = dt.Rows[0][10].ToString();
                    Site.Text = dt.Rows[0][11].ToString();
                    DateCreate.Text = dt.Rows[0][12].ToString();
                    DateEdit.Text = dt.Rows[0][13].ToString();
                    da.Dispose();
                    conn.Close();
                }
            }
        }

        private void btExport_Click(object sender, RoutedEventArgs e) // Экспорт данных из datagid в Excel
        {
            Excel.Application excel = new Excel.Application();
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
            string path = "C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\ExportToTxt\\exportCursomer.txt";
            StreamWriter sw = new StreamWriter(path);
            DataTable dt = Select1("Select * from Proekt.[Заказчик]");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("[Номер заказчика]:" + dt.Rows[i][0].ToString());
                sw.WriteLine("[Название компаний]:" + dt.Rows[i][1].ToString());
                sw.WriteLine("[Юридический адрес]:" + dt.Rows[i][2].ToString());
                sw.WriteLine("[Фактический адрес]:" + dt.Rows[i][3].ToString());
                sw.WriteLine("[ИНН]:" + dt.Rows[i][4].ToString());
                sw.WriteLine("[КПК]:" + dt.Rows[i][5].ToString());
                sw.WriteLine("[Расчетный счёт]:" + dt.Rows[i][6].ToString());
                sw.WriteLine("[Корреспондентский счёт]:" + dt.Rows[i][7].ToString());
                sw.WriteLine("[Представитель]:" + dt.Rows[i][8].ToString());
                sw.WriteLine("[Телефон представителя]:" + dt.Rows[i][9].ToString());
                sw.WriteLine("[Адрес электронной почты]:" + dt.Rows[i][10].ToString());
                sw.WriteLine("[Сайт]:" + dt.Rows[i][11].ToString());
                sw.WriteLine("[Дата и время появления записи]:" + dt.Rows[i][12].ToString());
                sw.WriteLine("[Дата и время изменения записи]:" + dt.Rows[i][13].ToString());
                sw.WriteLine(" ");
            }
            sw.Close();
            Process.Start("notepad.exe", path);
        }

        public DataTable Select1(string selectSql) // Подсоединение к базе данных для экспорта в txt
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(BdCon.Con);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSql;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dt);
            return dt;
        }

        private void btReport_Click(object sender, RoutedEventArgs e) // Экспорт данных из datagid в txt
        {
            if (txtBox1.Text != "")
            {
                new ReportClient(txtBox1.Text).ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Введите в поисковик Номер заказчика");
            }
        }
    } 
}
