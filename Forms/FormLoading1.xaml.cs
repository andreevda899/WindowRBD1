using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowRBD1.Class;
using WindowRBD1.FormsCreate;
using WindowRBD1.FormsMain;
using WindowRBD1.FormsMainOperator;

namespace WindowRBD1.Forms
{
    public partial class FormLoading1 : Window
    {
        public FormLoading1(string FIO)
        {
            InitializeComponent();
            lineProgress();
            usertype = FIO;
        }

        string usertype;

        public static void checkTime() //секундомер
        {
            string time = string.Format("{0:mm:ss}", DateTime.Now);

            int minute = Int32.Parse(string.Format("{0:mm}", DateTime.Now));
            string sec = string.Format("{0:ss}", DateTime.Now);

            if (minute <= 20)
                minute++;
            else
                minute = 01;

            string timer = minute + ":" + sec;

            if (minute < 10)
                timer = "0" + minute + ":" + sec;

            while (true)
            {
                time = string.Format("{0:mm:ss}", DateTime.Now);
                if (timer.Equals(time))
                    Environment.Exit(0);
                
            }
        }

        private async void lineProgress() // Запуск ProgressBar
        {
            for (int i = 0; i <= 100; i++)
            {
                pgbEstatus.Value = i * (100 / 100);
                await Task.Delay(50);
                label2.Content = i + "%";
            }

            if (usertype == "Area")
            {
                this.Hide();
                Area red = new Area();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Cursomer")
            {
                this.Hide();
                Cursomer red = new Cursomer();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Contract")
            {
                this.Hide();
                Contract red = new Contract();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Equipment")
            {
                this.Hide();
                Equipment red = new Equipment();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Generative")
            {
                this.Hide();
                Generative red = new Generative();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Meaning")
            {
                this.Hide();
                Meaning red = new Meaning();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Measuring")
            {
                this.Hide();
                Measuring red = new Measuring();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Personal")
            {
                this.Hide();
                Personal red = new Personal();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Methodology")
            {
                this.Hide();
                Methodology red = new Methodology();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Order")
            {
                this.Hide();
                Order red = new Order();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Picket")
            {
                this.Hide();
                Picket red = new Picket();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Profile")
            {
                this.Hide();
                Profile red = new Profile();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Project")
            {
                this.Hide();
                Project red = new Project();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Supervisor")
            {
                this.Hide();
                Supervisor red = new Supervisor();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Telemetry")
            {
                this.Hide();
                Telemetry red = new Telemetry();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Worker")
            {
                this.Hide();
                Worker red = new Worker();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Chief")
            {
                this.Hide();
                Chief red = new Chief();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Driver")
            {
                this.Hide();
                Driver red = new Driver();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Engineer")
            {
                this.Hide();
                Engineer red = new Engineer();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }



            if (usertype == "Area1")
            {
                this.Hide();
                Area1 red = new Area1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }


            if (usertype == "Meaning1")
            {
                this.Hide();
                Meaning1 red = new Meaning1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Cursomer1")
            {
                this.Hide();
                Cursomer1 red = new Cursomer1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Contract1")
            {
                this.Hide();
                Contract1 red = new Contract1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Generative1")
            {
                this.Hide();
                Generative1 red = new Generative1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Measuring1")
            {
                this.Hide();
                Measuring1 red = new Measuring1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Picket1")
            {
                this.Hide();
                Picket1 red = new Picket1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Profile1")
            {
                this.Hide();
                Profile1 red = new Profile1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Project1")
            {
                this.Hide();
                Project1 red = new Project1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Telemetry1")
            {
                this.Hide();
                Telemetry1 red = new Telemetry1();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Back")
            {
                this.Hide();
                MainWindow red = new MainWindow();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }

            if (usertype == "Back1")
            {
                this.Hide();
                MainWindowOperator red = new MainWindowOperator();
                red.ShowDialog();
                Thread thread = new Thread(checkTime);
                thread.Start();
            }
        }
    }
}
