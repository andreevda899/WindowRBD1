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
using WindowRBD1.FormsMainOperator;

namespace WindowRBD1.Forms
{
    public partial class MainWindowOperator : System.Windows.Window
    {
        public MainWindowOperator()
        {
            InitializeComponent();
            
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

        private void btClose_Click(object sender, RoutedEventArgs e) // закрытие формы
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            dataGridView1.IsReadOnly = true;
        }
    }
}
