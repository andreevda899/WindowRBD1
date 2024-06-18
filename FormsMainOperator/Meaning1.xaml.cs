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
using static System.Windows.Forms.MonthCalendar;
using WindowRBD1.FormsCreate.Meaning.intermediateResult;
using WindowRBD1.FormsCreate.Meaning;
using WindowRBD1.FormsEdit.Meaning;
using WindowRBD1.Forms;
using static ClosedXML.Excel.XLPredefinedFormat;
using WindowRBD1.Отчёты;
using System.Windows.Forms;

namespace WindowRBD1.FormsMainOperator
{
    public partial class Meaning1 : System.Windows.Window
    {
        public Meaning1()
        {
            InitializeComponent();
            CmTab1();
        }

        string con = BdCon.Con;
        public string RoleBox;
        public string NameBox;
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable dtSales = new DataTable();
        string sql;
        string tr;
        string Number1, NumberPicket1, Index1, Size1;
        
	    public string str3;
        int uri;
        public static bool flag = false;

        private void СlearCommand_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            flag = true;

            dataGridView1.UnselectAll();

            if (flag == true) { 
                txtNumberArea.Content = "";
                txtArea.Content = "";
                txtProfile.Content = "";
                txtPerimeterLength.Content = "";
                flag = false;
            }
        }

        private void Transform1Command_Click(object sender, RoutedEventArgs e)  //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.Трансформанта1";

            uri = 18;

            Number.Content = "Номер Трансформанты измерения 1";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения трансформанты (ρτ)";

            Number1 = "[Номер Трансформанты измерения 1]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения трансформанты (ρτ)]";

            tr = "Трансформанта1.txt";

            cmbTab.ItemsSource = new string[] { "Номер Трансформанты измерения 1", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.Трансформанта1";
            NameBox = "[Номер Трансформанты измерения 1]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.Трансформанта1");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Трансформанта1"].DefaultView;
            }
        }

        private void Transform2Command_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.Трансформанта2";

            uri = 19;

            Number.Content = "Номер Трансформанты измерения 2";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения трансформанты (ρτ)";

            Number1 = "[Номер Трансформанты измерения 2]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения трансформанты (ρτ)]";

            tr = "Трансформанта2.txt";

            cmbTab.ItemsSource = new string[] { "Номер Трансформанты измерения 2", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.Трансформанта2";
            NameBox = "[Номер Трансформанты измерения 2]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.Трансформанта2");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Трансформанта2"].DefaultView;
            }
        }

        private void Transform3Command_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.Трансформанта3";

            uri = 20;

            Number.Content = "Номер Трансформанты измерения 3";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения трансформанты (ρτ)";

            Number1 = "[Номер Трансформанты измерения 3]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения трансформанты (ρτ)]";

            tr = "Трансформанта3.txt";

            cmbTab.ItemsSource = new string[] { "Номер Трансформанты измерения 3", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.Трансформанта3";
            NameBox = "[Номер Трансформанты измерения 3]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.Трансформанта3");
                dataGridView1.ItemsSource = ds.Tables["Proekt.Трансформанта3"].DefaultView;
            }
        }

        private void finalCommand_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.[Окончательный результат]";

            uri = 21;

            Number.Content = "Номер Окончательного результата";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения измерения(ЭДС)";

            Number1 = "[Номер Окончательного результата]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения измерения(ЭДС)]";

            tr = "Окончательный результат.txt";

            cmbTab.ItemsSource = new string[] { "Номер Окончательного результата", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.[Окончательный результат]";
            NameBox = "[Номер Окончательного результата]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Окончательный результат]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Окончательный результат]"].DefaultView;
            }
        }

        private void IntermediateResultCommand_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.[Промежуточный результат]";

            uri = 22;

            Number.Content = "Номер Промежуточного результата";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения измерения(ЭДС)";

            Number1 = "[Номер Промежуточного результата]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения измерения(ЭДС)]";

            tr = "Промежуточный результат.txt";

            cmbTab.ItemsSource = new string[] { "Номер Промежуточного результата", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.[Промежуточный результат]";
            NameBox = "[Номер Промежуточного результата]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Промежуточный результат]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Промежуточный результат]"].DefaultView;
            }
        }

        private void IntermediateResult1Command_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.[Промежуточный результат 1]";

            uri = 23;

            Number.Content = "Номер Промежуточного результата 1";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения измерения(ЭДС)";

            Number1 = "[Номер Промежуточного результата 1]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения измерения(ЭДС)]";

            tr = "Промежуточный результат 1.txt";

            cmbTab.ItemsSource = new string[] { "Номер Промежуточного результата 1", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.[Промежуточный результат 1]";
            NameBox = "[Номер Промежуточного результата 1]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Промежуточный результат 1]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Промежуточный результат 1]"].DefaultView;
            }
        }

        private void IntermediateResult2Command_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.[Промежуточный результат 2]";

            uri = 24;

            Number.Content = "Номер Промежуточного результата 2";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения измерения(ЭДС)";

            Number1 = "[Номер Промежуточного результата 2]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения измерения(ЭДС)]";

            tr = "Промежуточный результат 2.txt";

            cmbTab.ItemsSource = new string[] { "Номер Промежуточного результата 2", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.[Промежуточный результат 2]";
            NameBox = "[Номер Промежуточного результата 2]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Промежуточный результат 2]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Промежуточный результат 2]"].DefaultView;
            }
        }

        private void IntermediateResult3Command_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            sql = "select * from Proekt.[Промежуточный результат 3]";

            uri = 25;

            Number.Content = "Номер Промежуточного результата 3";
            NumberPicket.Content = "Номер Пикета";
            Index.Content = "Индекс Пикета";
            Size.Content = "Значения измерения(ЭДС)";

            Number1 = "[Номер Промежуточного результата 3]";
            NumberPicket1 = "[Номер Пикета]";
            Index1 = "[Индекс Пикета]";
            Size1 = "[Значения измерения(ЭДС)]";

            tr = "Промежуточный результат 3.txt";

            cmbTab.ItemsSource = new string[] { "Номер Промежуточного результата 3", "Номер Пикета", "Индекс Пикета" };
            RoleBox = "Proekt.[Промежуточный результат 3]";
            NameBox = "[Номер Промежуточного результата 3]";
            using (SqlConnection conn = new SqlConnection(con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Промежуточный результат 3]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Промежуточный результат 3]"].DefaultView;
            }
        }

        private void BackCommand_Click(object sender, RoutedEventArgs e) //Происходит заполнение данными datagrid
        {
            this.Hide();
            new FormLoading1("Back1").ShowDialog();
        }



        private void CmTab1()
        {
            if (cmbTab.Text == "Номер Трансформанты измерения 1")
            {
                str3 = "[Номер Трансформанты измерения 1]";
            }

            if (cmbTab.Text == "Номер Трансформанты измерения 2")
            {
                str3 = "[Номер Трансформанты измерения 2]";
            }

            if (cmbTab.Text == "Номер Трансформанты измерения 3")
            {
                str3 = "[Номер Трансформанты измерения 3]";
            }

            if (cmbTab.Text == "Номер Окончательного результата")
            {
                str3 = "[Номер Окончательного результата]";
            }

            if (cmbTab.Text == "Номер Промежуточного результата")
            {
                str3 = "[Номер Промежуточного результата]";
            }

            if (cmbTab.Text == "Номер Промежуточного результата 1")
            {
                str3 = "[Номер Промежуточного результата 1]";
            }

            if (cmbTab.Text == "Номер Промежуточного результата 2")
            {
                str3 = "[Номер Промежуточного результата 2]";
            }

            if (cmbTab.Text == "Номер Промежуточного результата 3")
            {
                str3 = "[Номер Промежуточного результата 3]";
            }

            if (cmbTab.Text == "Номер Пикета")
            {
                str3 = "[Номер Пикета]";
            }

            if (cmbTab.Text == "Индекс Пикета")
            {
                str3 = "[Индекс Пикета]";
            }

            if (cmbTab.Text == "Значения трансформанты (ρτ)")
            {
                str3 = "[Значения трансформанты (ρτ)]";
            }

            if (cmbTab.Text == "Значения измерения(ЭДС)")
            {
                str3 = "[Значения измерения(ЭДС)]";
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

        private void btClose_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Hide();
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

            if (flag == false)
            {
                DataRowView rowView = dataGridView1.SelectedValue as DataRowView;
                string str = "Select * from " + RoleBox + " where " + NameBox.ToString() + " = '" + rowView[0].ToString() + "'";

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
                    txtPerimeterLength.Content = dt.Rows[0][3].ToString();

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
                sheet1.Columns[j + 1].ColumnWidth = 15;
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

        private void AddCommand_Click_1(object sender, RoutedEventArgs e)
        {

            if (RoleBox == "Proekt.Трансформанта1")
            {
                CreateTransformant1 rt = new CreateTransformant1();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.Трансформанта2")
            {
                CreateTransformant2 rt = new CreateTransformant2();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.Трансформанта3")
            {
                CreateTransformant3 rt = new CreateTransformant3();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Окончательный результат]")
            {
                CreatefinalResult rt = new CreatefinalResult();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат]")
            {
                CreateIntermediateResult rt = new CreateIntermediateResult();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 1]")
            {
                CreateIntermediateResult1 rt = new CreateIntermediateResult1();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 2]")
            {
                CreateIntermediateResult2 rt = new CreateIntermediateResult2();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 3]")
            {
                CreateIntermediateResult3 rt = new CreateIntermediateResult3();
                rt.ShowDialog();
            }
        }

        private void EditCommand_Click_1(object sender, RoutedEventArgs e)
        {
            if (RoleBox == "Proekt.Трансформанта1")
            {
                EditTransformant1 rt = new EditTransformant1();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.Трансформанта2")
            {
                EditTransformant2 rt = new EditTransformant2();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.Трансформанта3")
            {
                EditTransformant3 rt = new EditTransformant3();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Окончательный результат]")
            {
                EditfinalResult rt = new EditfinalResult();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат]")
            {
                EditIntermediateResult rt = new EditIntermediateResult();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 1]")
            {
                EditIntermediateResult1 rt = new EditIntermediateResult1();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 2]")
            {
                EditIntermediateResult2 rt = new EditIntermediateResult2();
                rt.ShowDialog();
            }

            if (RoleBox == "Proekt.[Промежуточный результат 3]")
            {
                EditIntermediateResult3 rt = new EditIntermediateResult3();
                rt.ShowDialog();
            }
        }

        private void DeleteCommand_Click_1(object sender, RoutedEventArgs e)
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
                System.Windows.MessageBox.Show("Введите в поисковик " + Number.Content + " для удаления");
            }
        }

        private void ExportToTxt_Click(object sender, RoutedEventArgs e) // Экспорт данных из datagid в txt
        {
            string path = "C:\\Users\\Пользователь\\Desktop\\WindowRBD1\\ExportToTxt\\" + tr.ToString();
            StreamWriter sw = new StreamWriter(path);
            DataTable dt = Select1("Select * from " + RoleBox);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine(Number1 + ":" + dt.Rows[i][0].ToString());
                sw.WriteLine(NumberPicket1 + ":" + dt.Rows[i][1].ToString());
                sw.WriteLine(Index1 + ":" + dt.Rows[i][2].ToString());
                sw.WriteLine(Size1 + ":" + dt.Rows[i][3].ToString());
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
                if (RoleBox == "[Трансформанта1]")
                {
                    new ReportTransformant1(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Трансформанта2]")
                {
                    new ReportTransformant2(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Трансформанта3]")
                {
                    new ReportTransformant3(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Окончательный результат]")
                {
                    new ReportfinalResult(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Промежуточный результат]")
                {
                    new ReportIntermediateResult(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Промежуточный результат 1]")
                {
                    new ReportIntermediateResult1(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Промежуточный результат 2]")
                {
                    new ReportIntermediateResult2(txtBox1.Text).ShowDialog();
                }

                if (RoleBox == "[Промежуточный результат 3]")
                {
                    new ReportIntermediateResult3(txtBox1.Text).ShowDialog();
                }
            }

            else
            {
                System.Windows.MessageBox.Show("Введите в поисковик " + Number.Content);
            }
        }
    }
}
