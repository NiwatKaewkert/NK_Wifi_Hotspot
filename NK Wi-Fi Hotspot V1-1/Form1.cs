using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace NK_Wi_Fi_Hotspot_V1_1
{
    public partial class Form1 : Form
    {
        Process settingCMD = new Process();
        Form3 help = new Form3();

        string name = System.IO.File.ReadAllText(@"name.txt");
        string password = System.IO.File.ReadAllText(@"password.txt");

        public Form1()
        {
            Thread splash = new Thread(new ThreadStart(SplashScreen));
            splash.Start();

            Thread.Sleep(4000);

            settingCMD.StartInfo.UseShellExecute = false;
            settingCMD.StartInfo.CreateNoWindow = true;
            settingCMD.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            InitializeComponent();

            name_textBox.Text = name;
            password_textBox.Text = password;
        }
        public void SplashScreen()
        {
            Application.Run(new Form2());
        }
        public void Process_1()
        {
            progressBar.Increment(1);
            settingCMD.StartInfo.FileName = "netsh.exe";
            settingCMD.StartInfo.Arguments = "wlan stop hostednetwork" ;
            settingCMD.Start();
            settingCMD.WaitForExit();

            Process_2();

        }
        public void Process_2()
        {
            progressBar.Increment(1);
            settingCMD.StartInfo.FileName = "netsh.exe";
            settingCMD.StartInfo.Arguments = "wlan set hostednetwork mode=allow ssid="+name_textBox.Text+" key="+password_textBox.Text;
            settingCMD.Start();
            settingCMD.WaitForExit();

            Process_3();
        }
        public void Process_3()
        {
            progressBar.Increment(1);
            settingCMD.StartInfo.FileName = "netsh.exe";
            settingCMD.StartInfo.Arguments = "wlan start hostednetwork" ;
            settingCMD.Start();
            settingCMD.WaitForExit();

            panel.Visible = true;
            progressBar.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;

            name_textBox.Enabled = false;
            password_textBox.Enabled = false;

            start_stop_button.Text = "Stop"; 
        }
        public void stopProcess()
        {
            progressBar.Increment(1);
            settingCMD.StartInfo.FileName = "netsh.exe";
            settingCMD.StartInfo.Arguments = "wlan stop hostednetwork";
            settingCMD.Start();
            settingCMD.WaitForExit();

            panel.Visible = true;
            progressBar.Visible = false;
            checkBox1.Visible = true;
            checkBox2.Visible = true;

            name_textBox.Enabled = true;
            password_textBox.Enabled = true;

            savepass();

            start_stop_button.Text = "Start"; 
        }
        public void savepass()
        {
            if(checkBox2.Checked == true)
            {
                System.IO.File.WriteAllText(@"password.txt", password_textBox.Text);
                System.IO.File.WriteAllText(@"name.txt", name_textBox.Text);
            }
            else
            {
                System.IO.File.WriteAllText(@"password.txt", "");
                System.IO.File.WriteAllText(@"name.txt", "");
            }
        }
        private void start_stop_button_Click(object sender, EventArgs e)
        {
            if (start_stop_button.Text == "Start")
            {
                if(name_textBox.TextLength < 2)
                {
                    MessageBox.Show("Please insert Hotspot Name more than 1", "Hotspot Name Error");
                }
                else if (password_textBox.TextLength < 8)
                {
                    MessageBox.Show("Please insert Password more than 8", "Hotspot Name Error");
                }
                else
                {
                    panel.Visible = false;
                    progressBar.Visible = true;
                    Process_1();
                }
            }
            
            else
            {
                stopProcess();
            }  
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                password_textBox.UseSystemPasswordChar = false;

            else
                password_textBox.UseSystemPasswordChar = true;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            savepass();
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.ShowBalloonTip(2000);
                this.Hide();
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            help.Show();
        }
    }
}
