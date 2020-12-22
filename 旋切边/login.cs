using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 旋切边
{
    public partial class login : Form
    {       
        public string password = string.Empty;
        public login()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "123456") 
            {
                global.adm = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            global.adm = false ;
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
