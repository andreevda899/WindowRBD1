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
using System.IO.Packaging;

namespace WindowRBD1.Forms
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CursomerCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Cursomer").ShowDialog();
        }

        private void MeaningCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Meaning").ShowDialog();
        }

        private void ProjectCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Project").ShowDialog();
        }

        private void PersonalCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Personal").ShowDialog();
        }

        private void ContractCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Contract").ShowDialog();
        }

        private void OrderCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Order").ShowDialog();
        }

        private void EngineerCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Engineer").ShowDialog();
        }

        private void ChiefCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Chief").ShowDialog();
        }

        private void WorkerCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Worker").ShowDialog();
        }

        private void DriverCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Driver").ShowDialog();
        }

        private void SupervisorCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Supervisor").ShowDialog();
        }

        private void MeasuringCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Measuring").ShowDialog();
        }

        private void GenerativeCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Generative").ShowDialog();
        }

        private void TelemetryCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Telemetry").ShowDialog();
        }

        private void MethodologyCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Methodology").ShowDialog();
        }

        private void PicketCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Picket").ShowDialog();
        }

        private void AreaCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Area").ShowDialog();
        }

        private void ProfileCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Profile").ShowDialog();
        }

        private void EquipmentCommand_Click(object sender, RoutedEventArgs e) //Происходит закрытие формы и открытие другой
        {
            this.Hide();
            new FormLoading1("Equipment").ShowDialog();
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
