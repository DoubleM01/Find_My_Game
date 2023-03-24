using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Data.OleDb;
using System.Net.Mail;

namespace Find_My_Game
{
    public partial class Form1 : Form
    {

        //string urlAddress = "http://google.com";
        stores gaming_stores = new stores();
        bool hint_ready = true;
        string[] keys = new string[3]
        {
            "ABC123-456XFD-BUE333",
            "ABC123-456XFD-BUE134",
            "ABC123-456XFD-BUE012"

        };
        string current_key = Find_My_Game.Properties.Settings.Default.Serial_number;

        public Form1()
        {
            InitializeComponent();
            if(current_key == null)
            {
                // this.Hide();
                this.Enabled = false;
                key_check k = new key_check();

                k.Show();
            }
            if (!ValidateLicenceKey(current_key))
            {
                this.Enabled = false;
                key_check k = new key_check();
                
                    k.Show();
               
            }


        }
        
        bool ValidateLicenceKey(string key)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (key ==keys[i])
                {
                    int v = DateTime.Now.Date.CompareTo(DateTime.Parse(Find_My_Game.Properties.Settings.Default.End_Date));
                    if (v <= 0)
                    {
                        
                        return true;
                    }
                    
                }
            }
            
            return false;
        }
        private void SearchBtn_Click(object sender, EventArgs e)
        {
          
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.SmallImageList = imageList1;
            

        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool created = false;
        public DataTable DataTableGetTable(string game, string price)
        {
           // if (!created)
           // {


                // Here we create a DataTable with four columns.  
                DataTable table = new DataTable();
                table.Columns.Add("Store", typeof(string));
                table.Columns.Add("Game", typeof(string));
                table.Columns.Add("Price", typeof(string));
            //}
            // Here we add 6 DataRows.  
            table.Rows.Add(25, "Paint Data");
            //table.Rows.Add(26, "Contact Data");
          //  table.Rows.Add(27, "find Data");
           // table.Rows.Add(28, "Paint Data");
//            table.Rows.Add(29, "Contact Data");
            return table;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

           /* if (textBox1.Text == "Enter Game Name" )
            {
               textBox1.Clear();
               textBox1.ForeColor = Color.Black;
            }
            else if (textBox1.Text == "")
            {

                hint_ready = false;


            }
            else
            {
               
            }*/
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }
        void hint(TextBox T_Box, string txt)
        {
            if (T_Box.Text == "" )
            {
                T_Box.ForeColor = Color.Gray;
                T_Box.Text = txt;
                T_Box.Select(T_Box.TextLength, 0);
                hint_ready = false;
            }
            else
            {
                if (hint_ready)
                {
                    T_Box.Text="";
                    T_Box.ForeColor = Color.Black;
                }
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        //string[] name = new string[4];
        string game = "";
        string[] links = new string[4];
        string[,] results = new string[4, 4]; // search-results{"store-name, game-name, price}

        string[] names;
        void CallSearch()
        {
            names = new string[4];


            int c = 0;

            foreach (var control in groupBox1.Controls.OfType<CheckBox>())
            {

                if (((CheckBox)control).Checked)
                {
                    if (c < 4)
                    {


                        names[c] = control.Text;
                        c++;

                    }
                }
                else
                {
                    if (c < 4)
                    {

                        names[c] = "no";
                        c++;
                    }
                }


            }

            listView1.Items.Clear();
            gaming_stores.search(textBox1.Text, names);
            results = gaming_stores.search_results;

            for (int i = 0; i < names.Length; i++)
            {

                if (names[i] != null)
                {
                    if (names[i] != "no")
                    {


                        for (int j = 0; j < names.Length; j++)
                        {

                            if (gaming_stores.logo[j, 0].ToLower() == results[i, 0].ToLower())
                            {

                                listView1.Items.Add("(" + results[i, 0] + ") " + results[i, 1] + " " + results[i, 2].Replace(" ", ""), gaming_stores.logo[j, 1]);
                            }

                        }
                    }
                }

            }
            results = null;
            pictureBox7.Enabled = true;
            pictureBox8.Enabled = true;

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            CallSearch();



        }

            private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
        public string idp;
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
           
        }
        string selected_result;
        private void listView1_Click(object sender, EventArgs e)
        {
             
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        string link = "";
        void openlink()
        {
            for (int i = 0; i <= 3; i++)
            {
                if (selected_result.Contains("(" + gaming_stores.search_results[i, 0] + ")"))
                {
                    link = gaming_stores.search_results[i, 3];
                    System.Diagnostics.Process.Start(link);
                    break;
                }
            }


        }
        private void websearchbtn_Click(object sender, EventArgs e)
        {

            openlink();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
                selected_result = listView1.FocusedItem.ToString();
               
                if (selected_result != null)
                {

                    websearchbtn.Enabled = true;

                }
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string tittle = "Prices List for " + textBox1.Text;
            string content = "";
            for (int i = 0; i <= 3; i++)
            {
                if (names[i] != null)
                {
                    if (names[i] != "no")
                    {
                        string store_n = gaming_stores.search_results[i, 0];
                        string g_price = gaming_stores.search_results[i, 2];
                        string g_link = gaming_stores.search_results[i, 3];
                        content = content + "\n store: " + store_n + " --> " + g_price + " to purchase: " + g_link;
                    }
                }
            }
            if (textBox2.Text != "")
            {
                try
                {
                    Sendmail(textBox2.Text, tittle, content);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }

        }

        public static void Sendmail(string to_mail, string subject, string body)
        {
            string from_mail = "fmg.projectbue@gmail.com";
            string password = "BUE2022.";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from_mail, password),
                EnableSsl = true
            };
           
            client.Send(from_mail,to_mail, subject, body);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }
        void saveCSV(string filepath)
        {
            using (var stream = File.CreateText(filepath))
            {
                string heade_row = string.Format("{0},{1},{2},{3}", "Game", "Store", "Price", "Link");
                stream.WriteLine(heade_row);
                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] != null)
                    {
                        if (names[i] != "no")
                        {
                            string game_na = gaming_stores.search_results[i, 1];
                            string store = gaming_stores.search_results[i, 0];
                            string price = gaming_stores.search_results[i, 2];
                            string link = gaming_stores.search_results[i, 3];
                            string row = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", game_na,store, price.Replace(" ",""), link);
                            stream.WriteLine(row);
                        }
                    }
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            saveCSV(saveFileDialog1.FileName);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        void ShowSettings()
        {
            settings settingsform = new settings();
            settingsform.Show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }
    }
}
