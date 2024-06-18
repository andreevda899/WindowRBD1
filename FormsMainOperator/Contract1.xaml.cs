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
using WindowRBD1.FormsMain;
using WindowRBD1.Отчёты;
using WindowRBD1.Forms;
using DocumentFormat.OpenXml.Presentation;
using static System.Windows.Forms.MonthCalendar;

namespace WindowRBD1.FormsMainOperator
{
    public partial class Contract1 : System.Windows.Window
    {
        public Contract1()
        {
            InitializeComponent();
            ContractCommand();
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

        private void СlearCommand_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            flag = true;

            dataGridView1.UnselectAll();

            if (flag == true)
            {
                txtNumberContract.Content = "";
                txtNameContract.Content = "";
                txtBeginnings.Content = "";
                txtEndings.Content = "";
                txtCost.Content = "";
                DateCreate.Text = "";
                DateEdit.Text = "";
                flag = false;
            }
        }

        private void CursomerCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Cursomer1").ShowDialog();
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

        private void ContractCommand()
        {
            sql = "select * from Proekt.Договор";
            uri = 3;
            cmbTab.ItemsSource = new string[] { "Номер договора", "Наименование договора" };
            RoleBox = "Proekt.Договор";
            using (SqlConnection conn = new SqlConnection(con))
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

        private void CmTab1()
        {
            if (cmbTab.Text == "Номер договора")
            {
                str3 = "[Номер договора]";
            }

            if (cmbTab.Text == "Наименование договора")
            {
                str3 = "[Наименование договора]";
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
                dataAdapter.Fill(ds, str3);
                dataGridView1.ItemsSource = ds.Tables[str3].DefaultView;
            }
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
            CreateContract form1 = new CreateContract();
            form1.Show();
        }

        private void EditCommand_Click(object sender, RoutedEventArgs e) //Происходит открытие формы
        {
            EditContract form1 = new EditContract();
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
      			    MessageBox.Show("Введите в поисковик Номер договора для удаления");
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
            if(flag == false) { 
                DataRowView rowView = dataGridView1.SelectedValue as DataRowView;
                string str = "Select * from Proekt.Договор where [Номер договора] = " + rowView[0].ToString();

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    txtNumberContract.Content = dt.Rows[0][0].ToString();
                    txtNameContract.Content = dt.Rows[0][1].ToString();
                    txtBeginnings.Content = dt.Rows[0][2].ToString();
                    txtEndings.Content = dt.Rows[0][3].ToString();
                    txtCost.Content = dt.Rows[0][4].ToString();
                    DateCreate.Text = dt.Rows[0][5].ToString();
                    DateEdit.Text = dt.Rows[0][6].ToString();
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
            string path = "C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\ExportToTxt\\exportContract.txt";
            StreamWriter sw = new StreamWriter(path);
            DataTable dt = Select1("Select * from Proekt.Договор");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("[Номер договора]:" + dt.Rows[i][0].ToString());
                sw.WriteLine("[Наименование договора]:" + dt.Rows[i][1].ToString());
                sw.WriteLine("[Начала]:" + dt.Rows[i][2].ToString());
                sw.WriteLine("[Окончания]:" + dt.Rows[i][3].ToString());
                sw.WriteLine("[Стоимость]:" + dt.Rows[i][4].ToString());
                sw.WriteLine("[Дата и время появления записи]:" + dt.Rows[i][5].ToString());
                sw.WriteLine("[Дата и время изменения записи]:" + dt.Rows[i][5].ToString());
                sw.WriteLine("");
            }
            sw.Close();
            Process.Start("notepad.exe", path);
        }

        public DataTable Select1(string selectSql)// Подсоединение к базе данных для экспорта в txt
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
                new ReportContract(txtBox1.Text).ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите в поисковик Номер договора");
            }
        }
    }
}
