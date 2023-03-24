using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
namespace Find_My_Game
{
    
    
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
            UpdateStartUpColor();

        }
        void UpdateStartUpColor()
        {
            bool current_startup = Find_My_Game.Properties.Settings.Default.startup;
            
            if (current_startup)
            {
                pictureBox1.BackColor = Color.Green;

            }
            else
            {
                pictureBox1.BackColor = Color.Red;
            }
        }
        private void SetStartup(bool enable)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enable)
                rk.SetValue("FMG", Application.ExecutablePath);
            else
                rk.DeleteValue("FMG", false);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool current_startup = Find_My_Game.Properties.Settings.Default.startup;
            SetStartup(!current_startup);
            Find_My_Game.Properties.Settings.Default.startup = !current_startup;
            Properties.Settings.Default.Save();
            UpdateStartUpColor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string end_date = Find_My_Game.Properties.Settings.Default.End_Date;
            DialogResult d = MessageBox.Show("your serial number is valid unitl \n" + end_date + "\n if you want to change it choose yes \n or click no to ignore ","Serial number",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                key_check k = new key_check();
                this.Enabled = false;
                this.Hide();
                //k.Controls.Find()
                foreach (Control c in k.Controls)
                {
                    if (c.Name == "button3")
                    {
                        c.Enabled = true;
                    }
                }
                        k.Show();
            }
        }
    }
}
