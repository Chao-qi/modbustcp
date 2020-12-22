using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 旋切边
{
    public partial class Tolerance : Form
    {
        public Tolerance()
        {
            InitializeComponent();
        }

        

        private void Tolerance_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = global.tcpPLC.readD("256");
                textBox2.Text = global.tcpPLC.readD("260");
                textBox3.Text = global.tcpPLC.readD("210");
                textBox4.Text = global.tcpPLC.readD("212");
                textBox8.Text = global.tcpPLC.readD("100");
                textBox7.Text = global.tcpPLC.readD("252");
                textBox6.Text = global.tcpPLC.readD("250");
                textBox5.Text = global.tcpPLC.readD("264");
            }
            catch (Exception)
            {
                MessageBox.Show("plc链接错误");
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show(this, "要保存当前参数吗？", "是否保存？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (dr)
            {
                case System.Windows.Forms.DialogResult.Yes://保存修改    
                    try
                    {
                        global.tcpPLC.writeD("256", textBox1.Text);
                        global.tcpPLC.writeD("260", textBox2.Text);
                        global.tcpPLC.writeD("210", textBox3.Text);
                        global.tcpPLC.writeD("212", textBox4.Text);
                        global.tcpPLC.writeD("100", textBox8.Text);
                        global.tcpPLC.writeD("252", textBox7.Text);
                        global.tcpPLC.writeD("250", textBox6.Text);
                        global.tcpPLC.writeD("264", textBox5.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("plc链接错误");
                    }
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
