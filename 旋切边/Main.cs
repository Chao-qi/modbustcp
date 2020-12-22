using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
//using System.IO;
//using System.Runtime.InteropServices;
//using System.IO.Ports;

namespace 旋切边
{
    public partial class Main : Form
    {
        private Alarm almfrm = null;
        private double intOKCounts = 0;
        private double intNGCounts = 0;
        private double intTotalCounts = 0;
       // private string ccdBuffer;
      //  private string ccdBuffOK;
        private string[] oldState=new string[16];
       // private bool ccdCheck;
        private double[] ccdData = new double[100];
       // private Int32 Num = 0;
        private double[] Weight = new double[100];
        private double[] FinalWt = new double[100];
     
       // private string _2DCodebuffer;
        
        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            global.tcpPLC = new tcp3U();

            if (!global.tcpPLC.tcpConnect("192.168.1.33"))
            {
                MessageBox.Show("PLC连接错误!");
            }
        } 
        
        private void 手动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manual manualfrm = new Manual();
            manualfrm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            #region 报警触发
            global.alarmString = global.tcpPLC.readD("20");


            global.alarmString = Convert.ToString(Convert.ToInt32(global.alarmString), 2).PadLeft(4, '0');
            if (global.alarmString != "0000")
            //报警发生
            {
                if (almfrm.Visible == false)
                {
                    almfrm.Visible = true;                              //报警框弹出
                }
            }
            else
            {
                if (almfrm.Visible == true)
                {
                    almfrm.Visible = false;
                }
            }

            #endregion
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show(this, "要退出当前界面吗？", "是否退出？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (dr)
            {
                case System.Windows.Forms.DialogResult.Yes://保存修改    
                    this.Close();
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;
            }
        }

        private void 计数清零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intOKCounts = 0;
            intNGCounts = 0;
            intTotalCounts = 0;
           // pointCount = 0;//良率
        }
        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login admin = new login();
            admin.ShowDialog();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
            if (global.adm)
            {
                参数设置ToolStripMenuItem.Visible = true;
                手动ToolStripMenuItem.Visible = true;
            }
            else
            {
                参数设置ToolStripMenuItem.Visible = false;
                手动ToolStripMenuItem.Visible = false;
            }
        }

        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tolerance tl = new Tolerance();
            tl.ShowDialog();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Transparent)
            {
                global.tcpPLC.writeD("0", "1");
                button1.Text = "自动中";
                button1.BackColor = Color.SpringGreen;
            }
            else if (button1.BackColor == Color.SpringGreen)
            {
                global.tcpPLC.writeD("0", "0");
                button1.Text = "手动中";
                button1.BackColor = Color.Transparent;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.Transparent)
            {
                global.tcpPLC.SetM("64");
                button4.BackColor = Color.SpringGreen;
            }
            else if (button4.BackColor == Color.SpringGreen)
            {
                global.tcpPLC.RstM("64");
                button4.BackColor = Color.Transparent;
            }
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)//预压
        {
            global.tcpPLC.SetM("92");
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("92");
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)//本压
        {
            global.tcpPLC.SetM("93");
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("93");
        }
        private void button9_MouseDown(object sender, MouseEventArgs e)//伺服上升
        {
            global.tcpPLC.SetM("61");
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("61");
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)//伺服下降
        {
            global.tcpPLC.SetM("60");
        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("60");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
