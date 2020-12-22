using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace 旋切边
{
    class tcp3U
    {
        private TcpClient tcpClient;
        private NetworkStream netStream;

        public bool tcpConnect(string ip)//连接
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 502);
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(iPEndPoint);
                netStream = tcpClient.GetStream();
            }
            catch
            {
                return false;
                //MessageBox.Show("网络连接异常！");
            }
            return true;
        }
        public string readD(string address)//讀取D
        {
            #region 发送数据
            string addressHex = string.Empty;
            string address1, address2;

            string numAddress = address;
            Int32 finalAddress = Convert.ToInt32(numAddress);

            addressHex = Convert.ToString(finalAddress, 16).ToUpper().PadLeft(4, '0');
            address1 = addressHex.Substring(0, 2);//高位
            address2 = addressHex.Substring(2, 2);//低位

            byte[] bts = new byte[12];
            bts[0] = 0x00;
            bts[1] = 0x01;//事务元标识符 2个字节
            bts[2] = 0x00;
            bts[3] = 0x00;//协议标识符 2个字节
            bts[4] = 0x00;
            bts[5] = 0x06;//长度 2个字节
            bts[6] = 0xFF;//单元标识 1个字节
            bts[7] = 0x03;//命令码1个字节，读寄存器
            bts[8] = Convert.ToByte(address1, 16);
            bts[9] = Convert.ToByte(address2, 16); ;//寄存器起始地址 2个字节，高位在前，低位在后 1.0位0x08,2.0位0x10即整数位需*8
            bts[10] = Convert.ToByte("00", 16);
            bts[11] = Convert.ToByte("02", 16); ;//寄存器数量 2个字节，高位在前，低位在后

            netStream.Write(bts, 0, bts.Length);
            #endregion
            //Function.delay(10);
            #region 接收数据
            List<byte> getData = new List<byte>();
            int dwStart = System.Environment.TickCount;
            while (true)
            {
                try
                {
                    //获取数据                      
                    int revInf = netStream.ReadByte();
                    getData.Add(Convert.ToByte(revInf));
                    if (getData.Count == 13)//返回的字节数量
                    {
                        byte[] byteArr = getData.ToArray();
                        string str = string.Empty;
                        string strConv = string.Empty;
                        foreach (byte bt in byteArr)
                        {
                            str += String.Format("{0:X}", bt).PadLeft(2, '0');
                        }
                        if (str.Length == 26 && str.Substring(3, 1) == "1")
                        {
                            strConv = Convert.ToInt32(str.Substring(str.Length - 4, 4) + str.Substring(str.Length - 8, 4), 16).ToString();
                            getData.Clear();
                            return strConv;
                        }
                        else
                        {
                            getData.Clear();
                            return "0";
                        } 
                    }
                }
                catch
                {
                    MessageBox.Show("网络端口发生错误，连接已断开(接收)！", "提示");
                    //释放相关资源                    
                    if (netStream != null)
                    {
                        netStream.Dispose();
                    }
                    return "Rev error";
                }
            }
            #endregion
        }

        public string readM(string address)//讀取M
        {
            #region 发送数据
            string addressHex = string.Empty;
            string address1, address2;

            string numAddress = address;
            Int32 finalAddress = Convert.ToInt32(numAddress);

            addressHex = Convert.ToString(finalAddress, 16).ToUpper().PadLeft(4, '0');
            address1 = addressHex.Substring(0, 2);//高位
            address2 = addressHex.Substring(2, 2);//低位

            byte[] bts = new byte[12];
            bts[0] = 0x00;
            bts[1] = 0x02;//事务元标识符 2个字节
            bts[2] = 0x00;
            bts[3] = 0x00;//协议标识符 2个字节
            bts[4] = 0x00;
            bts[5] = 0x06;//长度 2个字节
            bts[6] = 0xFF;//单元标识 1个字节
            bts[7] = 0x01;//命令码1个字节，读M
            bts[8] = Convert.ToByte(address1, 16);
            bts[9] = Convert.ToByte(address2, 16); ;//寄存器起始地址 2个字节，高位在前，低位在后 1.0位0x08,2.0位0x10即整数位需*8
            bts[10] = Convert.ToByte("00", 16);
            bts[11] = Convert.ToByte("01", 16); ;//寄存器数量 2个字节，高位在前，低位在后
            netStream.Write(bts, 0, bts.Length);
            #endregion

            #region 接收数据
            List<byte> getData = new List<byte>();
            int dwStart = System.Environment.TickCount;
            while (true)
            {
                try
                {
                    int revInf = netStream.ReadByte();
                    getData.Add(Convert.ToByte(revInf));
                    if (getData.Count == 10)
                    {
                        byte[] byteArr = getData.ToArray();
                        string strConv = string.Empty;
                        foreach (byte bt in byteArr)
                        {
                            strConv += String.Format("{0:X}", bt).PadLeft(2, '0');
                        }
                        if (strConv.Length == 20)
                        {
                            strConv = strConv.Substring(19, 1);
                        }
                        netStream.Flush();
                        getData.Clear();
                        return strConv;
                    }
                }
                catch
                {
                    MessageBox.Show("网络端口发生错误，连接已断开(接收)！", "提示");
                    //释放相关资源                    
                    if (netStream != null)
                    {
                        netStream.Dispose();
                    }
                    return "Rev error";
                }
            }
            #endregion
        }

        public string writeD(string address, string value)//写入D
        {
            #region 发送数据
            string addressHex = string.Empty;
            string address1, address2;
            string valueHex = string.Empty;
            string value1, value2, value3, value4;

            string numAddress = address;
            Int32 finalAddress = Convert.ToInt32(numAddress);

            addressHex = Convert.ToString(finalAddress, 16).ToUpper().PadLeft(4, '0');
            address1 = addressHex.Substring(0, 2);//高位
            address2 = addressHex.Substring(2, 2);//低位

            valueHex = Convert.ToString(Convert.ToInt32(value), 16).ToUpper().PadLeft(8, '0');
            value1 = valueHex.Substring(0, 2);//高位
            value2 = valueHex.Substring(2, 2);//低位
            value3 = valueHex.Substring(4, 2);//高位
            value4 = valueHex.Substring(6, 2);//低位

            byte[] bts = new byte[17];
            bts[0] = 0x00;
            bts[1] = 0x03;//事务元标识符 2个字节
            bts[2] = 0x00;
            bts[3] = 0x00;//协议标识符 2个字节
            bts[4] = 0x00;
            bts[5] = 0x0B;//长度 2个字节
            bts[6] = 0xFF;//单元标识 1个字节
            bts[7] = 0x10;//命令码1个字节，写多个寄存器
            bts[8] = Convert.ToByte(address1, 16);
            bts[9] = Convert.ToByte(address2, 16); ;//寄存器起始地址 2个字节，高位在前，低位在后 1.0位0x08,2.0位0x10即整数位需*8
            bts[10] = Convert.ToByte("0", 16);
            bts[11] = Convert.ToByte("2", 16); ;//寄存器数量N， 2个字节，高位在前，低位在后
            bts[12] = Convert.ToByte("4", 16); ;//字节数N*2， 1个字节
            bts[13] = Convert.ToByte(value3, 16);
            bts[14] = Convert.ToByte(value4, 16);
            bts[15] = Convert.ToByte(value1, 16);
            bts[16] = Convert.ToByte(value2, 16); ;//寄存器值 2*N个字节，高位在前，低位在后
            netStream.Write(bts, 0, bts.Length);
            #endregion

            #region 接收数据
            List<byte> getData = new List<byte>();
            int dwStart = System.Environment.TickCount;
            while (true)
            {
                if (System.Environment.TickCount - dwStart > 1000)
                {
                    return "overTime";
                }

                try
                {
                    //获取数据                      
                    int revInf = netStream.ReadByte();
                    if (netStream.DataAvailable)
                    {
                        getData.Add(Convert.ToByte(revInf));
                    }
                    else
                    {
                        getData.Add(Convert.ToByte(revInf));
                        //转换为字符串形式
                        byte[] byteArr = getData.ToArray();
                        string strConv = string.Empty;
                        foreach (byte bt in byteArr)
                        {
                            strConv += String.Format("{0:X}", bt).PadLeft(2, '0');
                        }
                        getData.Clear();
                        return strConv;
                    }
                }
                catch
                {
                    MessageBox.Show("网络端口发生错误，连接已断开(接收)！", "提示");
                    //释放相关资源                    
                    if (netStream != null)
                    {
                        netStream.Dispose();
                    }
                    return "Rev error";
                }
            }
            #endregion
        }

        public string SetM(string address)//置位M
        {
            #region 发送数据
            string addressHex = string.Empty;
            string address1, address2;

            string numAddress = address;
            Int16 finalAddress = Convert.ToInt16(numAddress);

            addressHex = Convert.ToString(finalAddress, 16).ToUpper().PadLeft(4, '0');
            address1 = addressHex.Substring(0, 2);//高位
            address2 = addressHex.Substring(2, 2);//低位

            byte[] bts = new byte[14];
            bts[0] = 0x00;
            bts[1] = 0x04;//事务元标识符 2个字节
            bts[2] = 0x00;
            bts[3] = 0x00;//协议标识符 2个字节
            bts[4] = 0x00;
            bts[5] = 0x08;//长度 2个字节
            bts[6] = 0xFF;//单元标识 1个字节
            bts[7] = 0x0F;//命令码1个字节，写M
            bts[8] = Convert.ToByte(address1, 16);
            bts[9] = Convert.ToByte(address2, 16); ;//寄存器起始地址 2个字节，高位在前，低位在后 1.0位0x08,2.0位0x10即整数位需*8
            bts[10] = Convert.ToByte("00", 16);
            bts[11] = Convert.ToByte("01", 16); ;//寄存器值 2*N个字节，高位在前，低位在后
            bts[12] = Convert.ToByte("01", 16);
            bts[13] = Convert.ToByte("01", 16);
            netStream.Write(bts, 0, bts.Length);
            #endregion
            //Function.delay(10);
            #region 接收数据
            List<byte> getData = new List<byte>();
            int dwStart = System.Environment.TickCount;
            while (true)
            {
                if (System.Environment.TickCount - dwStart > 1000)
                {
                    return "overTime";
                }

                try
                {
                    //获取数据                      
                    int revInf = netStream.ReadByte();
                    if (netStream.DataAvailable)
                    {
                        getData.Add(Convert.ToByte(revInf));
                    }
                    else
                    {
                        getData.Add(Convert.ToByte(revInf));
                        //转换为字符串形式
                        byte[] byteArr = getData.ToArray();
                        string strConv = string.Empty;
                        foreach (byte bt in byteArr)
                        {
                            strConv += String.Format("{0:X}", bt).PadLeft(2, '0');
                        }
                        getData.Clear();
                        //netStream.Flush();
                        return strConv;
                    }
                }
                catch
                {
                    MessageBox.Show("网络端口发生错误，连接已断开(接收)！", "提示");
                    //释放相关资源                    
                    if (netStream != null)
                    {
                        netStream.Dispose();
                    }
                    return "Rev error";
                }
            }
            #endregion
        }

        public string RstM(string address)//复位M
        {
            #region 发送数据
            string addressHex = string.Empty;
            string address1, address2;

            string numAddress = address;
            Int16 finalAddress = Convert.ToInt16(numAddress);

            addressHex = Convert.ToString(finalAddress, 16).ToUpper().PadLeft(4, '0');
            address1 = addressHex.Substring(0, 2);//高位
            address2 = addressHex.Substring(2, 2);//低位

            byte[] bts = new byte[14];
            bts[0] = 0x00;
            bts[1] = 0x04;//事务元标识符 2个字节
            bts[2] = 0x00;
            bts[3] = 0x00;//协议标识符 2个字节
            bts[4] = 0x00;
            bts[5] = 0x08;//长度 2个字节
            bts[6] = 0xFF;//单元标识 1个字节
            bts[7] = 0x0F;//命令码1个字节，写M
            bts[8] = Convert.ToByte(address1, 16);
            bts[9] = Convert.ToByte(address2, 16); ;//寄存器起始地址 2个字节，高位在前，低位在后 1.0位0x08,2.0位0x10即整数位需*8
            bts[10] = Convert.ToByte("00", 16);
            bts[11] = Convert.ToByte("01", 16); ;//寄存器值 2*N个字节，高位在前，低位在后
            bts[12] = Convert.ToByte("01", 16);
            bts[13] = Convert.ToByte("00", 16);
            netStream.Write(bts, 0, bts.Length);
            #endregion

            #region 接收数据
            List<byte> getData = new List<byte>();
            int dwStart = System.Environment.TickCount;
            while (true)
            {
                if (System.Environment.TickCount - dwStart > 1000)
                {
                    return "overTime";
                }

                try
                {
                    //获取数据                      
                    int revInf = netStream.ReadByte();
                    if (netStream.DataAvailable)
                    {
                        getData.Add(Convert.ToByte(revInf));
                    }
                    else
                    {
                        getData.Add(Convert.ToByte(revInf));
                        //转换为字符串形式
                        byte[] byteArr = getData.ToArray();
                        string strConv = string.Empty;
                        foreach (byte bt in byteArr)
                        {
                            strConv += String.Format("{0:X}", bt).PadLeft(2, '0');
                        }
                        getData.Clear();
                        //netStream.Flush();
                        return strConv;
                    }
                }
                catch
                {
                    MessageBox.Show("网络端口发生错误，连接已断开(接收)！", "提示");
                    //释放相关资源                    
                    if (netStream != null)
                    {
                        netStream.Dispose();
                    }
                    return "Rev error";
                }
            }
            #endregion
        }
    }

}
