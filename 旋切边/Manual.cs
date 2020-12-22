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
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)//pg开关电
        {
            global.tcpPLC.SetM("50");
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("50");
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)//pg链接
        {
            global.tcpPLC.SetM("1000");
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("1000");
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)//pg上切
        {
            global.tcpPLC.SetM("53");
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("53");
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)//pg下切
        {
            global.tcpPLC.SetM("52");
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("52");
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)//真空
        {
           
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)//吸盘真空
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

        private void button5_MouseDown(object sender, MouseEventArgs e)//led灯
        {
           
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            //if (button1.BackColor == Color.Transparent)
            //{
            //    global.tcpPLC.SetM("143");
            //    button1.BackColor = Color.SpringGreen;
            //}
            //else if (button1.BackColor == Color.SpringGreen)
            //{
            //    global.tcpPLC.RstM("143");
            //    button1.BackColor = Color.Transparent;
            //}
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)//本压
        {
            global.tcpPLC.SetM("93");
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("93");
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)//预压
        {
            global.tcpPLC.SetM("92");
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            global.tcpPLC.RstM("92");
        }

        private void button10_MouseDown(object sender, MouseEventArgs e) //复位
        {

        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {

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

        private void Manual_Load(object sender, EventArgs e)
        {

        }
    }  
}
