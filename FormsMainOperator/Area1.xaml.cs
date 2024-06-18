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

namespace WindowRBD1.FormsMainOperator
{
    public partial class Area1 : System.Windows.Window
    { public Area1()
        {
            InitializeComponent();
            AreaCommand();
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
                txtNumberArea.Content = "";
                txtArea.Content = "";
                txtProfile.Content = "";
                txtList.Content = "";
                txtPerimeterLength.Content = "";
                txtSizeArea.Content = "";
                dateBeginning.Content = "";
                dateEnd.Content = "";
                DateCreate.Text = "";
                DateEdit.Text = "";
                txtSupervisorData.Content = "";
                txtSupervisorOrder.Content = "";
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

        private void ContractCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Contract1").ShowDialog();
        }

        private void CmTab1() //Происходит заполнение данными datagrid
        {
            if (cmbTab.Text == "Номер площади")
            {
                str3 = "[Номер площади]";
            }

            if (cmbTab.Text == "Наименование площади")
            {
                str3 = "[Наименование площади]";
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

        private void AreaCommand() //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.Площади";
            uri = 5;
            cmbTab.ItemsSource = new string[] { "Номер площади", "Наименование площади" };
            RoleBox = "Proekt.Площади";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.Площади");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Площади"].DefaultView;
            } 
        }

        private void ProfileCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Profile1").ShowDialog();
        }
      
        private void AddCommand_Click(object sender, RoutedEventArgs e) //Происходит открытие формы
        {
            CreateArea form1 = new CreateArea();
            form1.Show();
        }

        private void EditCommand_Click(object sender, RoutedEventArgs e) //Происходит открытие формы
        {
            EditArea form1 = new EditArea();
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
      			    MessageBox.Show("Введите в поисковик Номер площади для удаления");
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
                string str = "Select * from Proekt.Площади where [Номер площади] = " + rowView[0].ToString();

                using (SqlConnection conn = new SqlConnection(BdCon.Con))
                {
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand();
                    myCommand.Connection = conn;
                    myCommand.CommandText = str;
                    SqlDataAdapter da = new SqlDataAdapter(myCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    txtNumberArea.Content = dt.Rows[0][0].ToString();
                    txtArea.Content = dt.Rows[0][1].ToString();
                    txtProfile.Content = dt.Rows[0][2].ToString();
                    txtList.Content = dt.Rows[0][3].ToString();
                    txtPerimeterLength.Content = dt.Rows[0][4].ToString();
                    txtSizeArea.Content = dt.Rows[0][5].ToString();
                    dateBeginning.Content = dt.Rows[0][6].ToString();
                    dateEnd.Content = dt.Rows[0][7].ToString();
                    DateCreate.Text = dt.Rows[0][8].ToString();
                    DateEdit.Text = dt.Rows[0][9].ToString();
                    txtSupervisorData.Content = dt.Rows[0][10].ToString();
                    txtSupervisorOrder.Content = dt.Rows[0][11].ToString();
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
            string path = "C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\ExportToTxt\\exportArea.txt";
            StreamWriter sw = new StreamWriter(path);
            DataTable dt = Select1("Select * from Proekt.Площади");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("[Номер площади]:" + dt.Rows[i][0].ToString());
                sw.WriteLine("[Наименование площади]:" + dt.Rows[i][1].ToString());
                sw.WriteLine("[Номер списка углов периметра]:" + dt.Rows[i][2].ToString());
                sw.WriteLine("[Величина площади окружённая периметром]:" + dt.Rows[i][3].ToString());
                sw.WriteLine("[Дата и время окончания работ]:" + dt.Rows[i][4].ToString());
                sw.WriteLine("[Дата и время появления записи]:" + dt.Rows[i][5].ToString());
                sw.WriteLine("[Дата и время изменения записи]:" + dt.Rows[i][6].ToString());
                sw.WriteLine("[Супервайзер полевых работ]:" + dt.Rows[i][7].ToString());
                sw.WriteLine("[Супервайзер обработки данных]:" + dt.Rows[i][8].ToString());
                sw.WriteLine("");
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
                new ReportArea(txtBox1.Text).ShowDialog();
            }
            else
            {
                MessageBox.Show("Введите в поисковик Номер площади");
            }
        }
    }
}
