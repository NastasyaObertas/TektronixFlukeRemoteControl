using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TektronixFlukeRemoteControl
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;



        // functions imported are documented in TekVISA Programmer Manual
        [DllImport("visa32.dll")]
        public static extern int viOpenDefaultRM(out int sesn);
        [DllImport("visa32.dll")]
        public static extern int viFindRsrc(int sesn, string expr, out int findList, out int retCount, StringBuilder instrDesc);
        [DllImport("visa32.dll")]
        public static extern int viFindNext(int findList, StringBuilder instrDesc);
        [DllImport("visa32.dll")]
        public static extern int viGetAttribute(int vi, int attribute, out byte attrState);
        [DllImport("visa32.dll")]
        public static extern int viSetAttribute(int vi, int attribute, byte attrState);
        [DllImport("visa32.dll")]
        public static extern int viOpen(int sesn, string rsrcName, int accessMode, int timeout, out int vi);
        [DllImport("visa32.dll")]
        public static extern int viClose(int vi);
        [DllImport("visa32.dll")]
        public static extern int viStatusDesc(int vi, int status, StringBuilder desc);
        [DllImport("visa32.dll")]
        public static extern int viRead(int vi, byte[] buf, int count, out int retCount);
        [DllImport("visa32.dll")]
        public static extern int viWrite(int vi, byte[] buf, int count, out int retCount);



        StringBuilder videsc = new StringBuilder(512);
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

        FormStatus CurrStatus = FormStatus.FirstStart;

        int Visa_session;

        int ItemCnt;
        int List;
        int Visa_connection;


        string GetCHValueCommd = "SOURce{0}:VOLTage:AMPLitude?";

        string SetCHValueCommd = "SOURce{0}:VOLTage:LEVel:IMMediate:AMPLitude {1}Vpp";

        string GetDevInfoCommd = "*idn?";

        string GetCHStateCommd = "OUTPut{0}?";
        string SetCHStateCommd = "OUTPut{0}:STATe {1}";



        //当前是VPP值还是rms值
        bool CurrVPP;

        bool CurrCH1;

        double CurrAMPValue_Vpp;

        double RMSConversion = Math.Sqrt(2.0) / 4.0;
        double VPPConversion = 4.0 / Math.Sqrt(2.0);


        /////////////////
        ///串口获取值部分
        private SerialPort ComDevice;

        //循环获取间隔时间
        int IntervalTime;

        //是否等待数据接收
        bool IsWaitRev;

        //0 无通讯, 1 获取数据 2 发vdc 3 发vac 4 发 adc
        int SerialCommStatus = 0;

        //0 未知, 1 vac 2 vdc 3adc
        int CurrSerialMeasurementStatus;

        //VAL?;FUNC1?
        byte[] GetFLUKEValue = new byte[] { 0x56, 0x41, 0x4c, 0x3f, 0x3b, 0x46, 0x55, 0x4E, 0x43, 0x31, 0x3f, 0x0d };
        //VDC
        byte[] SetFLUKEVDC = new byte[] { 0x56, 0x44, 0x43, 0x0d };
        //VAC
        byte[] SetFLUKEVAC = new byte[] { 0x56, 0x41, 0x43, 0x0d };
        //ADC
        byte[] SetFLUKEADC = new byte[] { 0x41, 0x44, 0x43, 0x0d };

        byte[] GetFLUKEDevInfo = new byte[] { 0x2a, 0x49, 0x44, 0x4e, 0x3f, 0x0d };


        //byte[] GetFunc = new byte[] { 0x46, 0x55, 0x4E, 0x43, 0x31, 0x3f, 0x0a };
        byte[] SerialReciveBuffer = new byte[100];
        int SerialReciveCount = 0;
        int SerialErrCount = 0;
        int SerialPinJIeCount = 0;

        /// <summary>
        /// 45表的交流电压RMS基准值
        /// </summary>
        double Serial_VAC_BaseValue = 4.0 / Math.Sqrt(2);

        /// <summary>
        /// 45表检测交流电压RMS的误差值
        /// </summary>
        double Serial_VAC_BaseDevValue = 0.0005;



        /// <summary>
        /// 45表的直流电压基准值
        /// </summary>
        double Serial_VDC_BaseValue = 8.0;

        /// <summary>
        /// 45表检测的直流电压误差值
        /// </summary>
        double Serial_VDC_BaseDevValue = 0.005;

        /// <summary>
        /// 45表的当前测量值
        /// </summary>
        double CurrSerialValueRMS;
        /// <summary>
        /// /////////
        /// 
        /// </summary>
        /// 

        bool CacheCH1_ON;
        bool CacheCH2_ON;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;




        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeFormStatus(FormStatus.Init);

            cmb_valutype.Items.Clear();

            cmb_valutype.Items.Add("V p-p");
            cmb_valutype.Items.Add("V rms");


            cmb_valutype.SelectedIndex = 0;
            CurrVPP = true;

            this.cmb_valutype.SelectedIndexChanged += new System.EventHandler(this.cmb_valutype_SelectedIndexChanged);

            rb_ch1.Checked = true;
            CurrCH1 = true;
            this.rb_ch1.CheckedChanged += new System.EventHandler(this.rb_ch_CheckedChanged);
            this.rb_ch2.CheckedChanged += new System.EventHandler(this.rb_ch_CheckedChanged);

            ComDevice = new SerialPort();


            cmb_sport.Items.AddRange(SerialPort.GetPortNames());

            if (cmb_sport.Items.Count > 0)
            {
                cmb_sport.SelectedIndex = 0;
            }
            else
            {
                cmb_sport.Text = "Serial port not detected";

                cmb_sport.Enabled = false;
                btn_openPort.Enabled = false;

            }
            //ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived2);

            gb_fluke.Text = "FLUKE 45";
            gb_tektronix.Text = "Tektronix";

            lb_serialErrCount.Text = SerialErrCount.ToString();
            lb_serialCount.Text = SerialReciveCount.ToString();
            lb_pinjieCount.Text = SerialPinJIeCount.ToString();

            this.Text = "Automatic control";

            btn_deva.MouseWheel += Btn_pdA_MouseWheel;
            btn_pua.MouseWheel += Btn_pdA_MouseWheel;

            btn_pub.MouseWheel += Btn_pdB_MouseWheel;
            btn_devb.MouseWheel += Btn_pdB_MouseWheel;

        }

        private void Btn_pdA_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (cb_bigchange.Checked)
                {
                    SetValue(+0.1);
                }
                else
                {
                    SetValue(+0.01);
                }


            }
            else
            {
                if (cb_bigchange.Checked)
                {
                    SetValue(-0.1);
                }
                else
                {
                    SetValue(-0.01);
                }

            }



        }


        private void Btn_pdB_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (cb_bigchange.Checked)
                {
                    SetValue(+0.05);
                }
                else
                {
                    SetValue(+0.001);
                }


            }
            else
            {
                if (cb_bigchange.Checked)
                {
                    SetValue(-0.05);
                }
                else
                {
                    SetValue(-0.001);
                }

            }



        }


        private void ChangeFormStatus(FormStatus formStatus)
        {

            if (CurrStatus == formStatus)
            {
                return;
            }
            CurrStatus = formStatus;

            switch (CurrStatus)
            {
                case FormStatus.Init:

                    btn_openRM.Enabled = true;

                    btn_findRs.Enabled = false;

                    btn_findNext.Enabled = false;
                    btn_conndev.Enabled = false;

                    btn_ch1set.Enabled = false;
                    btn_ch2set.Enabled = false;
                    rb_ch1.Enabled = false;
                    rb_ch2.Enabled = false;
                    cmb_valutype.Enabled = false;

                    btn_read.Enabled = false;

                    lb_value.Enabled = false;

                    lb_unit.Enabled = false;


                    break;
                case FormStatus.Open:

                    btn_openRM.Enabled = false;

                    btn_findRs.Enabled = true;

                    btn_findNext.Enabled = false;
                    btn_conndev.Enabled = false;

                    btn_ch1set.Enabled = false;
                    btn_ch2set.Enabled = false;
                    rb_ch1.Enabled = false;
                    rb_ch2.Enabled = false;
                    cmb_valutype.Enabled = false;

                    btn_read.Enabled = false;

                    lb_value.Enabled = false;

                    lb_unit.Enabled = false;


                    break;

                case FormStatus.CanConn:

                    btn_openRM.Enabled = false;

                    btn_findRs.Enabled = true;

                    btn_findNext.Enabled = false;
                    btn_conndev.Enabled = true;

                    btn_ch1set.Enabled = false;
                    btn_ch2set.Enabled = false;
                    rb_ch1.Enabled = false;
                    rb_ch2.Enabled = false;
                    cmb_valutype.Enabled = false;

                    btn_read.Enabled = false;

                    lb_value.Enabled = false;

                    lb_unit.Enabled = false;


                    break;


                case FormStatus.CanNextDev:

                    btn_openRM.Enabled = false;

                    btn_findRs.Enabled = true;

                    btn_findNext.Enabled = true;
                    btn_conndev.Enabled = true;

                    btn_ch1set.Enabled = false;
                    btn_ch2set.Enabled = false;
                    rb_ch1.Enabled = false;
                    rb_ch2.Enabled = false;
                    cmb_valutype.Enabled = false;

                    btn_read.Enabled = false;

                    lb_value.Enabled = false;

                    lb_unit.Enabled = false;
                    break;


                case FormStatus.ConnDev:

                    btn_openRM.Enabled = false;

                    btn_findRs.Enabled = false;

                    btn_findNext.Enabled = false;
                    btn_conndev.Enabled = true;

                    btn_ch1set.Enabled = true;
                    btn_ch2set.Enabled = true;
                    rb_ch1.Enabled = true;
                    rb_ch2.Enabled = true;
                    cmb_valutype.Enabled = true;

                    btn_read.Enabled = true;

                    lb_value.Enabled = true;

                    lb_unit.Enabled = true;

                    break;
                default:
                    break;
            }


        }


        private void btn_openRM_Click(object sender, EventArgs e)
        {


            int result = viOpenDefaultRM(out Visa_session);


            if (result == 0)
            {
                ChangeFormStatus(FormStatus.Open);
            }
            else
            {
                ChangeFormStatus(FormStatus.Init);
            }
        }

        private void btn_findRs_Click(object sender, EventArgs e)
        {

            int result = viFindRsrc(Visa_session, "?*", out List, out ItemCnt, videsc);



            //if (result == 0)
            //{
            tb_devName.Text = videsc.ToString();

            if (ItemCnt > 1)
            {
                ChangeFormStatus(FormStatus.CanNextDev);

            }
            else
            {
                ChangeFormStatus(FormStatus.CanConn);

            }


            //}
            //else
            //{
            //    ChangeFormStatus(FormStatus.Open);
            //}



        }

        private void btn_findNext_Click(object sender, EventArgs e)
        {
            int result = viFindNext(List, videsc);

            if (result == 0)
            {

                tb_devName.Text = videsc.ToString();

                ItemCnt--;

                if (ItemCnt == 1)
                {
                    ChangeFormStatus(FormStatus.CanConn);
                }
                else
                {
                    ChangeFormStatus(FormStatus.CanNextDev);

                }


            }
            else
            {
                ChangeFormStatus(FormStatus.Open);
            }

        }

        private void btn_conndev_Click(object sender, EventArgs e)
        {
            if (CurrStatus != FormStatus.ConnDev)
            {

                int result = viOpen(Visa_session, tb_devName.Text, 0, 5000, out Visa_connection);
                if (result == 0)
                {
                    ChangeFormStatus(FormStatus.ConnDev);
                    GetDevInfo();
                    ReadValue();

                    ONCHStatusChange(1, GetCHState(1), true);
                    ONCHStatusChange(2, GetCHState(2), true);
                    btn_conndev.Text = "Disconnect";

                }
            }
            else
            {
                int result = viClose(Visa_session);
                if (result == 0)
                {
                    ChangeFormStatus(FormStatus.Init);

                    btn_conndev.Text = "Connect";

                    btn_ch1set.BackColor = btn_read.BackColor;
                    btn_ch2set.BackColor = btn_read.BackColor;



                }
            }


        }

        //获取 通道状态
        private bool GetCHState(int chno)
        {
            byte[] b;

            b = encoding.GetBytes(string.Format(GetCHStateCommd, chno.ToString()));



            int result = viWrite(Visa_connection, b, b.Length, out int retCt);

            if (result != 0)
            {
                OnError("Communication error", result);
                return false;
            }
            result = viRead(Visa_connection, b, b.Length, out retCt);
            if (result != 0)
            {
                OnError("Communication error", result);
                return false;
            }

            string tempStr = encoding.GetString(b, 0, retCt);



            if (tempStr.Contains("1"))
            {
                return true;
            }

            return false;
        }




        public void ONCHStatusChange(int ch, bool isOn, bool isinit)
        {

            if (ch == 1)
            {

                if (CacheCH1_ON != isOn || isinit)
                {
                    CacheCH1_ON = isOn;
                    //btn_ch1set.Text = CacheCH1_ON ? "CH1 OFF" : "CH1 ON";
                    btn_ch1set.BackColor = CacheCH1_ON ? Color.Yellow : btn_read.BackColor;

                }


            }
            else if (ch == 2)
            {
                if (CacheCH2_ON != isOn || isinit)
                {
                    CacheCH2_ON = isOn;
                    // btn_ch2set.Text = CacheCH2_ON ? "CH2 OFF" : "CH2 ON";
                    btn_ch2set.BackColor = CacheCH2_ON ? Color.Aqua : btn_read.BackColor;
                }

            }

        }


        private void rb_ch_CheckedChanged(object sender, EventArgs e)
        {
            bool tempCurrCH1 = rb_ch1.Checked;

            if (tempCurrCH1 != CurrCH1)
            {

                CurrCH1 = tempCurrCH1;
                ReadValue();

                if (CurrCH1)
                {
                    //CacheCH1_ON = GetCHState(1);
                    ONCHStatusChange(1, GetCHState(1), false);


                }
                else
                {
                    //CacheCH2_ON = GetCHState(2);
                    ONCHStatusChange(2, GetCHState(2), false);
                }

            }




        }


        //切换通道状态
        private void btn_chSet_Click(object sender, EventArgs e)
        {

            Button temp = sender as Button;


            if (temp.Name.Contains("1"))
            {

                bool tempStatus = GetCHState(1);

                if (CacheCH1_ON == tempStatus)
                {
                    byte[] tb;

                    tb = encoding.GetBytes(string.Format(SetCHStateCommd, "1", CacheCH1_ON ? "OFF" : "ON"));
                    int result = viWrite(Visa_connection, tb, tb.Length, out int retCt);

                    if (result != 0)
                    {
                        OnError("Communication error", result);
                        return;
                    }

                }

                ONCHStatusChange(1, GetCHState(1), false);
            }
            else if (temp.Name.Contains("2"))
            {
                bool tempStatus = GetCHState(2);

                if (CacheCH2_ON == tempStatus)
                {
                    byte[] tb;

                    tb = encoding.GetBytes(string.Format(SetCHStateCommd, "2", CacheCH2_ON ? "OFF" : "ON"));
                    int result = viWrite(Visa_connection, tb, tb.Length, out int retCt);

                    if (result != 0)
                    {
                        OnError("Communication error", result);
                        return;
                    }

                }
                ONCHStatusChange(2, GetCHState(2), false);
            }







            //byte[] b;
            //if (CurrCH1)
            //{
            //    if (temp.Name.EndsWith("on"))
            //    {
            //        b = encoding.GetBytes(string.Format(SetCHStateCommd, "1", "ON"));
            //    }
            //    else
            //    {
            //        b = encoding.GetBytes(string.Format(SetCHStateCommd, "1", "OFF"));
            //    }



            //}
            //else
            //{
            //    if (temp.Name.EndsWith("on"))
            //    {
            //        b = encoding.GetBytes(string.Format(SetCHStateCommd, "2", "ON"));
            //    }
            //    else
            //    {
            //        b = encoding.GetBytes(string.Format(SetCHStateCommd, "2", "OFF"));
            //    }
            //}

            //int result = viWrite(Visa_connection, b, b.Length, out int retCt);

            //if (result != 0)
            //{
            //    OnError("通讯错误", result);
            //    return;
            //}
        }


        private void cmb_valutype_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool temp = (cmb_valutype.SelectedIndex == 0);

            if (temp != CurrVPP)
            {

                CurrVPP = temp;
                ReadValue();
            }

        }

        private void ChangeCurrShowValueUnit()
        {

            if (CurrVPP)
            {
                lb_value.Text = CurrAMPValue_Vpp.ToString("F3");

                lb_unit.Text = "V p-p";
            }
            else
            {
                lb_value.Text = (CurrAMPValue_Vpp * RMSConversion).ToString("F3");

                lb_unit.Text = "V rms";
            }




        }


        /// <summary>
        /// 设定值 参数是和当前的差值
        /// </summary>
        /// <param name="diffValue"></param>
        private void SetValue(double diffValue)
        {
            double setValue;


            double tempOldValue = CurrAMPValue_Vpp;

            if (CurrVPP)
            {
                setValue = Math.Round(CurrAMPValue_Vpp + diffValue, 3);
            }
            else
            {

                setValue = Math.Round((Math.Round(CurrAMPValue_Vpp * RMSConversion, 3) + diffValue) * VPPConversion, 3);


            }

            byte[] b;
            if (CurrCH1)
            {
                b = encoding.GetBytes(string.Format(SetCHValueCommd, "1", setValue.ToString()));

            }
            else
            {
                b = encoding.GetBytes(string.Format(SetCHValueCommd, "2", setValue.ToString()));
            }

            int result = viWrite(Visa_connection, b, b.Length, out int retCt);

            if (result != 0)
            {
                OnError("Communication error", result);
                return;
            }

            ReadValue();

            //如果没变化
            if (tempOldValue == CurrAMPValue_Vpp)
            {

                OnError("The communication is normal, but the value does not change due to the accuracy, which is modified twice", result);

                if (CurrVPP)
                {
                    setValue = Math.Round(CurrAMPValue_Vpp + diffValue + diffValue, 3);
                }
                else
                {

                    setValue = Math.Round((Math.Round(CurrAMPValue_Vpp * RMSConversion, 3) + diffValue + diffValue) * VPPConversion, 3);


                }


                if (CurrCH1)
                {
                    b = encoding.GetBytes(string.Format(SetCHValueCommd, "1", setValue.ToString()));

                }
                else
                {
                    b = encoding.GetBytes(string.Format(SetCHValueCommd, "2", setValue.ToString()));
                }

                result = viWrite(Visa_connection, b, b.Length, out retCt);

                if (result != 0)
                {
                    OnError("Communication error", result);
                    return;
                }

                ReadValue();

            }



        }


        private void ReadValue()
        {
            byte[] b;
            if (CurrCH1)
            {
                b = encoding.GetBytes(string.Format(GetCHValueCommd, "1"));

            }
            else
            {
                b = encoding.GetBytes(string.Format(GetCHValueCommd, "2"));
            }




            int result = viWrite(Visa_connection, b, b.Length, out int retCt);

            if (result != 0)
            {
                OnError("Communication error", result);
                return;
            }
            result = viRead(Visa_connection, b, b.Length, out retCt);
            if (result != 0)
            {
                OnError("Communication error", result);
                return;
            }

            string tempStr = encoding.GetString(b, 0, retCt);

            if (double.TryParse(tempStr, out double tempDouble))
            {
                CurrAMPValue_Vpp = Math.Round(tempDouble, 3);
                ChangeCurrShowValueUnit();
            }
            else
            {
                OnError("Parsing error, returned content" + tempStr, 0);


                return;
            }

        }


        private void GetDevInfo()
        {
            byte[] b = encoding.GetBytes(GetDevInfoCommd);

            int result = viWrite(Visa_connection, b, b.Length, out int retCt);

            if (result != 0)
            {
                OnError("Communication error", result);
                return;
            }

            b = new byte[50];

            result = viRead(Visa_connection, b, b.Length, out retCt);
            if (result != 0)
            {
                OnError("Communication error", result);
                return;
            }

            string tempStr = encoding.GetString(b, 0, retCt);

            // this.Text = tempStr.Substring(0, 18);

            gb_tektronix.Text = tempStr;

        }


        private void btn_read_Click(object sender, EventArgs e)
        {

            ReadValue();

        }


        public void OnError(string msg, int status)
        {
            MessageBox.Show(msg + ",status=" + status);

        }

        private void btn_deva_Click(object sender, EventArgs e)
        {
            if (cb_bigchange.Checked)
            {
                SetValue(-0.1);
            }
            else
            {
                SetValue(-0.01);
            }

            // SetValue(-0.01);
        }

        private void btn_devb_Click(object sender, EventArgs e)
        {

            if (cb_bigchange.Checked)
            {
                SetValue(-0.05);
            }
            else
            {
                SetValue(-0.001);
            }

            //SetValue(-0.001);
        }

        private void btn_pua_Click(object sender, EventArgs e)
        {

            if (cb_bigchange.Checked)
            {
                SetValue(0.1);
            }
            else
            {
                SetValue(0.01);
            }
            //SetValue(0.01);
        }

        private void btn_pub_Click(object sender, EventArgs e)
        {

            if (cb_bigchange.Checked)
            {
                SetValue(0.05);
            }
            else
            {
                SetValue(0.001);
            }
            // SetValue(0.001);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrStatus == FormStatus.ConnDev)
            {

                viClose(Visa_session);
            }
        }

        private void btn_openPort_Click(object sender, EventArgs e)
        {
            SerialReciveCount = 0;
            SerialErrCount = 0;
            SerialPinJIeCount = 0;



            if (ComDevice.IsOpen == false)
            {
                //设置串口相关属性
                ComDevice.PortName = cmb_sport.SelectedItem.ToString();
                ComDevice.BaudRate = 9600;
                ComDevice.Parity = Parity.None;
                ComDevice.DataBits = 8;
                ComDevice.StopBits = StopBits.One;
                try
                {
                    //开启串口
                    ComDevice.Open();
                    btn_startGetValue.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failed to open the serial port successfully", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btn_openPort.Text = "Close SPort";

                if (string.IsNullOrEmpty(tb_interval.Text))
                {
                    tb_interval.Text = "300";
                }

                lb_serialErrCount.Text = SerialErrCount.ToString();
                lb_serialCount.Text = SerialReciveCount.ToString();
                lb_pinjieCount.Text = SerialPinJIeCount.ToString();


                ComDevice.Write(GetFLUKEDevInfo, 0, GetFLUKEDevInfo.Length);

                IsWaitRev = true;

            }
            else
            {
                try
                {
                    SerialCommStatus = 0;
                    //关闭串口
                    ComDevice.Close();
                    btn_startGetValue.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Serial port closing error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btn_openPort.Text = "Open SPort";

            }
        }

        private void btn_startGetValue_Click(object sender, EventArgs e)
        {
            string intervalStr = tb_interval.Text;

            if (!int.TryParse(intervalStr, out int intervalvalue))
            {
                MessageBox.Show("Interval error");
                return;
            }


            IntervalTime = intervalvalue;
            SerialCommStatus = 1;
            SaveRevBuffStartOffset = 0;
            ThreadPool.QueueUserWorkItem(new WaitCallback(AutoGetValue));//执行线程池

            btn_startGetValue.Enabled = false;
        }


        //需要缓存 接收buff的每次接收的偏移量
        int SaveRevBuffStartOffset = 0;




        // 获取值 正常完整字符串 回复 如果有回显  VAL?;FUNC1?\r\n+22.473E+0;VDC\r\n=>\r\n
        // 没有回显 返回+22.473E+0;VDC\r\n=>\r\n

        // 设置命令  如果又回显 返回 VDC\r\n=>\r\n 
        //没有回显 返回 =>\r\n
        //错误响应 返回 ?>\r\n

        // 正常时 结尾应该是 0D 0A 3D 3E 0D 0A
        //错误时  结尾应该是 0D 0A 3F 3E 0D 0A


        //实际情况 极个别可能会有粘包 也就是最后一个字符不一定就是结束的


        //找到最后的标志位

        //返回设备信息 FLUKE, 45, 7620033, 1.7 D2.0\r\n=>\r\n


        // effectiveLen有效长度

        private int FindSenmentEndMark(int startIndex, int effectiveLen)
        {
            //每个数据包肯定是以=>\r\n或者?>\r\n结尾的 这个就是结尾的标志 通用的结尾标志即为 >\r\n
            //由于需要搜索到 包头和结尾 且需要记录最后一个包尾位置 因此 最佳方案还是从头向后搜索

            int i = startIndex;
            while (i < effectiveLen - 2)
            {
                if (SerialReciveBuffer[i] == 0x3e && SerialReciveBuffer[i + 1] == 0x0d && SerialReciveBuffer[i + 2] == 0x0a)
                {
                    return i;
                }
                else
                {
                    i++;
                }

            }


            return -1;

        }



        private void Com_DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {

            int revlen = ComDevice.Read(SerialReciveBuffer, SaveRevBuffStartOffset, SerialReciveBuffer.Length - SaveRevBuffStartOffset);

            //int endIndex = SaveRevBuffStartOffset + revlen - 1;
            SaveRevBuffStartOffset += revlen;

            //单段解析的结束标志索引 即>字符的位置
            int getEndMask;

            //单段解析的起始索引
            int parseOffset = 0;

            //累计的解析长度,用户最后移动数据
            int allParseLen = 0;
            getEndMask = FindSenmentEndMark(parseOffset, SaveRevBuffStartOffset);
            while (getEndMask > 0)
            {
                if (SerialReciveBuffer[getEndMask - 1] == 0x3d)
                {
                    SerialReciveCount++;
                    //正常的完整响应
                    //由于结束标志 前一个字符也知道了
                    //解析 就到+22.473E+0;VDC\r\n
                    string tempStr = encoding.GetString(SerialReciveBuffer, parseOffset, getEndMask - parseOffset - 1);


                    int getSegmentIndex = tempStr.LastIndexOf(';');
                    int getValueIndex = tempStr.LastIndexOf('?');
                    int valueIndex = 0;
                    if (getValueIndex > 0)
                    {           //如果 以VAL起始 且 包含? 则为 带回显的数据响应
                                //带问号 从最后一个问号 后面 加3开始解析 解析到分号

                        valueIndex = getValueIndex + 3;


                    }


                    //如果包含分号 则为返回数据响应     //如果没有分号 则为 命令响应
                    if (getSegmentIndex > 0)
                    {
                        //数据响应



                        int valueLen = 0;




                        valueLen = getSegmentIndex - valueIndex;
                        string tempdoubleStr = tempStr.Substring(valueIndex, valueLen);
                        string tempMeaStatus = tempStr.Substring(getSegmentIndex + 1, tempStr.Length - getSegmentIndex - 3);

                        if ("VAC".Equals(tempMeaStatus))
                        {
                            CurrSerialMeasurementStatus = 1;
                            tempMeaStatus = "V rms AC";

                        }
                        else if ("VDC".Equals(tempMeaStatus))
                        {
                            CurrSerialMeasurementStatus = 2;
                            tempMeaStatus = "V DC";
                        }
                        else if ("ADC".Equals(tempMeaStatus))
                        {
                            CurrSerialMeasurementStatus = 3;
                            tempMeaStatus = "A DC";
                        }
                        else
                        {
                            CurrSerialMeasurementStatus = 0;
                            // tempMeaStatus = "UNKNOW";
                        }


                        if (double.TryParse(tempdoubleStr, out CurrSerialValueRMS))
                        {
                            Color getValueTextColor_F;
                            Color getValueTextColor_B;
                            Color setValueButtomTextColor_Plus;
                            Color setValueButtomTextColor_Dev;

                            if (CurrSerialMeasurementStatus == 1)
                            {

                                if (Math.Abs(CurrSerialValueRMS - Serial_VAC_BaseValue) > Serial_VAC_BaseDevValue)
                                {
                                    getValueTextColor_F = Color.Black;
                                    getValueTextColor_B = Color.Yellow;


                                    if (CurrSerialValueRMS > Serial_VAC_BaseValue)
                                    {

                                        setValueButtomTextColor_Dev = Color.Red;

                                        setValueButtomTextColor_Plus = Color.Black;

                                    }
                                    else
                                    {
                                        setValueButtomTextColor_Dev = Color.Black;

                                        setValueButtomTextColor_Plus = Color.Red;


                                    }



                                }
                                else
                                {
                                    getValueTextColor_F = Color.White;
                                    getValueTextColor_B = Color.Green;

                                    setValueButtomTextColor_Dev = Color.Black;

                                    setValueButtomTextColor_Plus = Color.Black;
                                }
                            }
                            else if (CurrSerialMeasurementStatus == 2)
                            {
                                if (Math.Abs(CurrSerialValueRMS - Serial_VDC_BaseValue) > Serial_VDC_BaseDevValue)
                                {
                                    getValueTextColor_F = Color.Black;
                                    getValueTextColor_B = Color.Yellow;
                                }
                                else
                                {
                                    getValueTextColor_F = Color.White;
                                    getValueTextColor_B = Color.Green;
                                }

                                setValueButtomTextColor_Dev = Color.Black;

                                setValueButtomTextColor_Plus = Color.Black;

                            }
                            else
                            {
                                getValueTextColor_F = lb_serialCount.ForeColor;
                                getValueTextColor_B = lb_serialCount.BackColor;
                                setValueButtomTextColor_Dev = Color.Black;

                                setValueButtomTextColor_Plus = Color.Black;

                            }




                            this.Invoke(new EventHandler(delegate
                            {
                                lb_getPortUnit.ForeColor = getValueTextColor_F;
                                lb_getPortUnit.BackColor = getValueTextColor_B;

                                lb_serialCount.Text = SerialReciveCount.ToString();


                                btn_pua.ForeColor = setValueButtomTextColor_Plus;
                                btn_pub.ForeColor = setValueButtomTextColor_Plus;

                                btn_deva.ForeColor = setValueButtomTextColor_Dev;
                                btn_devb.ForeColor = setValueButtomTextColor_Dev;


                                lb_getPortValue.ForeColor = getValueTextColor_F;
                                lb_getPortValue.BackColor = getValueTextColor_B;
                                if (CurrSerialValueRMS > -1 && CurrSerialValueRMS < 1)
                                {
                                    lb_getPortValue.Text = DoubleToStr(CurrSerialValueRMS * 1000,7,2);
                                    lb_getPortUnit.Text = "m" + tempMeaStatus;
                                }
                                else
                                {
                                    lb_getPortValue.Text = DoubleToStr(CurrSerialValueRMS, 7, 2); 
                                    lb_getPortUnit.Text = tempMeaStatus;
                                }









                                //btn_vac.Enabled = true;
                                //btn_vdc.Enabled = true;

                            }));



                        }
                        else
                        {//解析数据失败
                            this.Invoke(new EventHandler(delegate
                            {
                                CurrSerialValueRMS = double.NaN;
                                lb_getPortValue.Text = "Parse ERR:" + tempdoubleStr;
                                lb_getPortValue.ForeColor = Color.Red;
                                lb_getPortUnit.Text = tempMeaStatus;
                                lb_serialCount.Text = SerialReciveCount.ToString();
                                //btn_vac.Enabled = true;
                                //btn_vdc.Enabled = true;
                            }));
                        }
                    }
                    else
                    {

                        int douhaoIndex = tempStr.IndexOf(',');

                        if (douhaoIndex > 0)
                        {



                            //获取设备info
                            this.Invoke(new EventHandler(delegate
                            {

                                gb_fluke.Text = tempStr.Substring(valueIndex);

                            }));
                        }
                        else
                        {
                            //命令响应
                            this.Invoke(new EventHandler(delegate
                            {

                                btn_vac.Enabled = true;
                                btn_vdc.Enabled = true;
                                btn_adc.Enabled = true;
                                lb_serialCount.Text = SerialReciveCount.ToString();



                                SerialCommStatus = 1;

                            }));





                        }


                    }



                }
                //else if (SerialReciveBuffer[getEndMask - 1] == 0x3f)
                //{
                //    //错误的的完整响应
                //}
                else
                {
                    //错误的的完整响应
                    SerialErrCount++;

                    if (SerialCommStatus != 1)
                    {
                        this.Invoke(new EventHandler(delegate
                        {

                            // btn_vac.Enabled = true;
                            // btn_vdc.Enabled = true;

                            //lb_serialCount.Text = SerialReciveCount.ToString();
                            lb_serialErrCount.Text = SerialErrCount.ToString();
                        }));




                        //SerialCommStatus = 1;
                    }

                    //错误的完整数据 结束




                }
                IsWaitRev = false;

                //还有后面\r\n 两个字符 所以加3
                allParseLen += (getEndMask - parseOffset + 3);
                //还有后面\r\n 两个字符 所以加3
                parseOffset = getEndMask + 3;

                getEndMask = FindSenmentEndMark(parseOffset, SaveRevBuffStartOffset);

            }


            if (allParseLen == 0)
            {
                return;
            }





            if (allParseLen == SaveRevBuffStartOffset)
            { //如果 结尾即是段结尾
                SaveRevBuffStartOffset = 0;

                return;
            }
            else
            {
                //把后面剩余部分移动到段起始
                SaveRevBuffStartOffset = SaveRevBuffStartOffset - allParseLen;

                for (int i = 0; i < SaveRevBuffStartOffset; i++)
                {
                    SerialReciveBuffer[0] = SerialReciveBuffer[allParseLen + i];
                }

                SerialPinJIeCount++;
                this.Invoke(new EventHandler(delegate
                {
                    lb_pinjieCount.Text = SerialPinJIeCount.ToString();
                }));
            }


        }


        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            int revlen = ComDevice.Read(SerialReciveBuffer, SaveRevBuffStartOffset, SerialReciveBuffer.Length - SaveRevBuffStartOffset);

            //int endIndex = SaveRevBuffStartOffset + revlen - 1;
            SaveRevBuffStartOffset += revlen;

            if (SerialReciveBuffer[SaveRevBuffStartOffset - 1] == 0x0a && SerialReciveBuffer[SaveRevBuffStartOffset - 2] == 0x0d && SerialReciveBuffer[SaveRevBuffStartOffset - 3] == 0x3e)
            {

                int parseStrLen = SaveRevBuffStartOffset - 4;

                string tempStr = encoding.GetString(SerialReciveBuffer, 0, parseStrLen);

                if (SerialReciveBuffer[SaveRevBuffStartOffset - 4] == 0x3d)
                {
                    //正常的完整数据 结束
                    SerialReciveCount++;

                    int getValueIndex = tempStr.LastIndexOf('?');
                    int getSegmentIndex = tempStr.LastIndexOf(';');


                    //如果时发送设置命令的反馈 字符串起始不是+-或数字 即 ascii编码大于41为字母
                    //应该为 命令反馈信息

                    //返回信息没有分号 都是命令反馈
                    if (getSegmentIndex < 0)
                    {
                        this.Invoke(new EventHandler(delegate
                        {

                            // btn_vac.Enabled = true;
                            // btn_vdc.Enabled = true;
                            lb_serialCount.Text = SerialReciveCount.ToString();
                        }));


                        SerialCommStatus = 1;
                    }
                    else
                    {
                        //如果时获取数据的反馈



                        int valueIndex = 0;
                        int valueLen = 0;

                        //如果是开启了回显 返回信息会有?
                        if (getValueIndex > 0)
                        {

                            //因为 实际数据是 ?\r\n 要跳过一共三个字符 
                            valueIndex = getValueIndex + 3;




                        }
                        else
                        {//未开启回显


                            valueIndex = 0;

                        }

                        valueLen = getSegmentIndex - valueIndex;
                        string tempdouble = tempStr.Substring(valueIndex, valueLen);
                        string tempMeaStatus = tempStr.Substring(getSegmentIndex + 1, tempStr.Length - getSegmentIndex - 3);

                        if ("VAC".Equals(tempMeaStatus))
                        {
                            CurrSerialMeasurementStatus = 1;
                            tempMeaStatus = "V rms AC";

                        }
                        else if ("VDC".Equals(tempMeaStatus))
                        {
                            CurrSerialMeasurementStatus = 2;
                            tempMeaStatus = "V DC";
                        }
                        else
                        {
                            CurrSerialMeasurementStatus = 0;
                            // tempMeaStatus = "UNKNOW";
                        }


                        if (double.TryParse(tempdouble, out CurrSerialValueRMS))
                        {
                            Color getValueTextColor_F;
                            Color getValueTextColor_B;
                            Color setValueButtomTextColor_Plus;
                            Color setValueButtomTextColor_Dev;

                            if (CurrSerialMeasurementStatus == 1)
                            {

                                if (Math.Abs(CurrSerialValueRMS - Serial_VAC_BaseValue) > Serial_VAC_BaseDevValue)
                                {
                                    getValueTextColor_F = Color.Black;
                                    getValueTextColor_B = Color.Yellow;


                                    if (CurrSerialValueRMS > Serial_VAC_BaseValue)
                                    {

                                        setValueButtomTextColor_Dev = Color.Red;

                                        setValueButtomTextColor_Plus = Color.Black;

                                    }
                                    else
                                    {
                                        setValueButtomTextColor_Dev = Color.Black;

                                        setValueButtomTextColor_Plus = Color.Red;


                                    }



                                }
                                else
                                {
                                    getValueTextColor_F = Color.White;
                                    getValueTextColor_B = Color.Green;

                                    setValueButtomTextColor_Dev = Color.Black;

                                    setValueButtomTextColor_Plus = Color.Black;
                                }
                            }
                            else if (CurrSerialMeasurementStatus == 2)
                            {
                                if (Math.Abs(CurrSerialValueRMS - Serial_VDC_BaseValue) > Serial_VDC_BaseDevValue)
                                {
                                    getValueTextColor_F = Color.Black;
                                    getValueTextColor_B = Color.Yellow;
                                }
                                else
                                {
                                    getValueTextColor_F = Color.White;
                                    getValueTextColor_B = Color.Green;
                                }

                                setValueButtomTextColor_Dev = Color.Black;

                                setValueButtomTextColor_Plus = Color.Black;

                            }
                            else
                            {
                                getValueTextColor_F = lb_serialCount.ForeColor;
                                getValueTextColor_B = lb_serialCount.BackColor;
                                setValueButtomTextColor_Dev = Color.Black;

                                setValueButtomTextColor_Plus = Color.Black;

                            }




                            this.Invoke(new EventHandler(delegate
                            {
                                lb_getPortValue.ForeColor = getValueTextColor_F;
                                lb_getPortValue.BackColor = getValueTextColor_B;
                                lb_getPortValue.Text = DoubleToStr(CurrSerialValueRMS, 7, 2);

                                lb_getPortUnit.ForeColor = getValueTextColor_F;
                                lb_getPortUnit.BackColor = getValueTextColor_B;
                                lb_getPortUnit.Text = tempMeaStatus;
                                lb_serialCount.Text = SerialReciveCount.ToString();


                                btn_pua.ForeColor = setValueButtomTextColor_Plus;
                                btn_pub.ForeColor = setValueButtomTextColor_Plus;

                                btn_deva.ForeColor = setValueButtomTextColor_Dev;
                                btn_devb.ForeColor = setValueButtomTextColor_Dev;




                                btn_vac.Enabled = true;
                                btn_vdc.Enabled = true;

                            }));
                        }
                        else
                        {
                            this.Invoke(new EventHandler(delegate
                            {
                                CurrSerialValueRMS = double.NaN;
                                lb_getPortValue.Text = "ERR:" + tempdouble;
                                lb_getPortValue.ForeColor = Color.Red;
                                lb_getPortUnit.Text = tempMeaStatus;
                                lb_serialCount.Text = SerialReciveCount.ToString();
                                btn_vac.Enabled = true;
                                btn_vdc.Enabled = true;
                            }));

                        }



                        //测试 不解析 直接显示/

                        //this.Invoke(new EventHandler(delegate
                        //{
                        //   // CurrSerialValueRMS = double.NaN;
                        //    lb_getPortValue.Text =  tempdouble;
                        //   // lb_getPortValue.ForeColor = Color.Red;
                        //    lb_getPortUnit.Text = tempMeaStatus;
                        //    lb_serialCount.Text = SerialReciveCount.ToString();
                        //    btn_vac.Enabled = true;
                        //    btn_vdc.Enabled = true;
                        //}));


                    }


                    SaveRevBuffStartOffset = 0;
                    IsWaitRev = false;

                }
                else
                {
                    SerialErrCount++;

                    if (SerialCommStatus != 1)
                    {
                        this.Invoke(new EventHandler(delegate
                        {

                            btn_vac.Enabled = true;
                            btn_vdc.Enabled = true;

                            //lb_serialCount.Text = SerialReciveCount.ToString();
                            lb_serialErrCount.Text = SerialErrCount.ToString();
                        }));




                        //SerialCommStatus = 1;
                    }

                    //错误的完整数据 结束


                    SaveRevBuffStartOffset = 0;
                    IsWaitRev = false;



                }


            }
            else
            {
                //包未完成

                //this.Invoke(new EventHandler(delegate
                //{

                //    lb_revstr.Text = revlen.ToString() + ":" + tempStr;
                //    //lb_ssendcount.Text = (++SerialReciveCount).ToString();
                //}));
                return;


            }


        }

        private void AutoGetValue(object s)
        {


            while (ComDevice.IsOpen)
            {

                //0 无通讯, 1 获取数据 2 发vdc 3 发vac 



                try
                {
                    switch (SerialCommStatus)
                    {

                        case 1:
                            //将消息传递给串口
                            ComDevice.Write(GetFLUKEValue, 0, GetFLUKEValue.Length);
                            break;
                        case 2:
                            ComDevice.Write(SetFLUKEVDC, 0, SetFLUKEVDC.Length);
                            break;
                        case 3:
                            ComDevice.Write(SetFLUKEVAC, 0, SetFLUKEVAC.Length);
                            break;
                        case 4:
                            ComDevice.Write(SetFLUKEADC, 0, SetFLUKEVAC.Length);
                            break;
                        default:
                            return;
                            //continue;

                    }


                }
                catch (Exception ex)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        btn_startGetValue.Enabled = true;
                    }));


                    Thread.Sleep(IntervalTime);
                    continue;
                }

                IsWaitRev = true;



                Thread.Sleep(IntervalTime);

                while (IsWaitRev)
                {
                    Thread.Sleep(IntervalTime / 100);
                }

            }


            this.Invoke(new EventHandler(delegate
            {
                btn_startGetValue.Enabled = true;
            }));



        }

        private void btn_vac_Click(object sender, EventArgs e)
        {

            if (CurrSerialMeasurementStatus == 1)
            {
                return;
            }


            if (SerialCommStatus == 1)
            {
                SerialCommStatus = 3;
                btn_vac.Enabled = false;
                btn_vdc.Enabled = false;
                btn_adc.Enabled = false;
            }

        }

        private void btn_vdc_Click(object sender, EventArgs e)
        {
            if (CurrSerialMeasurementStatus == 2)
            {
                return;
            }


            if (SerialCommStatus == 1)
            {
                SerialCommStatus = 2;
                btn_vac.Enabled = false;
                btn_vdc.Enabled = false;
                btn_adc.Enabled = false;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btn_mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cb_bigchange_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_bigchange.Checked)
            {
                btn_deva.Text = "-0.1";
                btn_devb.Text = "-0.05";

                btn_pua.Text = "+0.1";
                btn_pub.Text = "+0.05";
            }
            else
            {
                btn_deva.Text = "-0.01";
                btn_devb.Text = "-0.001";

                btn_pua.Text = "+0.01";
                btn_pub.Text = "+0.001";
            }
        }

        private void btn_adc_Click(object sender, EventArgs e)
        {
            if (CurrSerialMeasurementStatus == 3)
            {
                return;
            }


            if (SerialCommStatus == 1)
            {
                SerialCommStatus = 4;
                btn_vac.Enabled = false;
                btn_vdc.Enabled = false;
                btn_adc.Enabled = false;
            }
        }

        /// <summary>
        /// 按自定义的方式显示double字符串
        /// </summary>
        /// <param name="d">原始值</param>
        /// <param name="SignificantDigit">有效数字位数</param>
        /// <param name="decimalDigit">小数点后位数</param>
        /// <returns></returns>
        public static string DoubleToStr(double d, int SignificantDigit, int decimalDigit)
        {

            string tformat = "G" + SignificantDigit;


            if (double.IsNaN(d) || double.IsInfinity(d))
            {
                return d.ToString();
            }




            string temp = d.ToString(tformat);
            int tIndex = temp.LastIndexOf(".");

            if (tIndex == -1)
            {
                return temp + ".000";
            }

            return temp;

        }

    }

    public enum FormStatus
    {

        FirstStart, Init, Open, CanConn, CanNextDev, ConnDev

    }

}
