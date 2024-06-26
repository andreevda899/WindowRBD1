﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;

namespace WindowRBD1.Forms
{
    public partial class NumberGenerative : Window
    {
        public NumberGenerative()
        {
            InitializeComponent();
            dataGridView();
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void Button_Click(object sender, RoutedEventArgs e) // закрытие программы
        {
            this.Close();
        }

        private void dataGridView() //Происходит заполнение данными datagrid
        {
            string sql = "select * from Proekt.[Описание генераторной установки]";

            using (SqlConnection conn = new SqlConnection(BdCon.Con))
            {
                conn.Open();
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = conn;
                myCommand.CommandText = sql;

                ds.Clear();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(ds, "Proekt.[Описание генераторной установки]");
                dataGridView1.ItemsSource = ds.Tables["Proekt.[Описание генераторной установки]"].DefaultView;
            }
        }
    }
}
