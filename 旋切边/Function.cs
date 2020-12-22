using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 旋切边
{
    class Function
    {
        #region 延时函数
        public static void delay(uint ms)
        {
            int dwStart = System.Environment.TickCount;
            while (System.Environment.TickCount - dwStart < ms)
            {
                Application.DoEvents();
            }
        }
        #endregion

    }
}
