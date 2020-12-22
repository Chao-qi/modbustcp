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
    public partial class Alarm : Form
    {
        string[] oldState = new string[16];
        public Alarm()
        {
            InitializeComponent();
        }
        public void almAdd(string almMsg)
        {
            listBox1.Items.Add(almMsg);
        }
        public void almRemove(string almMsg)
        {
            listBox1.Items.Remove(almMsg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string[] newState = new string[16];
            newState[0] = global.alarmString.Substring(2, 1);
            if (oldState[0] != newState[0])
            {
                oldState[0] = newState[0];
                if (newState[0] == "1")
                {
                    this.almAdd("紧急停止");
                }
                else
                {
                    this.almRemove("紧急停止");
                }
            }

            newState[1] = global.alarmString.Substring(1, 1);
            if (oldState[1] != newState[1])
            {
                oldState[1] = newState[1];
                if (newState[1] == "1")
                {
                    this.almAdd("光幕报警");
                }
                else
                {
                    this.almRemove("光幕报警");
                }
            }

            newState[2] = global.alarmString.Substring(3, 1);
            if (oldState[2] != newState[2])
            {
                oldState[2] = newState[2];
                if (newState[2] == "1")
                {
                    this.almAdd("棱镜气缸报警");
                }
                else
                {
                    this.almRemove("棱镜气缸报警");
                }
            }

        }
        private void Alarm_Load(object sender, EventArgs e)
        {
            //timer1.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
