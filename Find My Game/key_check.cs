using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Find_My_Game
{
    public partial class key_check : Form
    {
        public key_check()
        {
            InitializeComponent();
        }
        string da;

        private void button1_Click(object sender, EventArgs e)
        {
            bool c = checkkey(textBox1.Text, ref da);
            if (!c)
            {
               // throw new Exception("Invalid License");
            }
            else
            {
               Find_My_Game.Properties.Settings.Default.Serial_number = textBox1.Text;
                Find_My_Game.Properties.Settings.Default.End_Date = da;
                Properties.Settings.Default.Save();

                Application.Restart();
            }
        }
        bool checkkey(string key, ref string Due)
        {
            string[,] keys = new string[3, 2]
        {
           {"ABC123-456XFD-BUE333","4/20/2022"},
            {"ABC123-456XFD-BUE134","4/30/2022"},
            { "ABC123-456XFD-BUE012","4/21/2022"}

    };
            if (!string.IsNullOrEmpty(key))
            {
                for (int i = 0; i < keys.GetLength(0); i++)
                {
                    
                    if (key == keys[i,0])
                    {
                        //MessageBox.Show(keys[i,0]);
                        // string dt = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                        int v =  DateTime.Now.Date.CompareTo(DateTime.Parse(keys[i, 1]));
                       // MessageBox.Show(v.ToString());
                        if (v < 0)
                        {
                            Due = keys[i, 1];
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void key_check_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
