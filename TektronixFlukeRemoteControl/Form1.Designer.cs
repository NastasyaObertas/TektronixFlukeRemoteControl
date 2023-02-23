
namespace TektronixFlukeRemoteControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_close = new System.Windows.Forms.Button();
            this.gb_tektronix = new System.Windows.Forms.GroupBox();
            this.btn_ch2set = new System.Windows.Forms.Button();
            this.btn_ch1set = new System.Windows.Forms.Button();
            this.cb_bigchange = new System.Windows.Forms.CheckBox();
            this.rb_ch2 = new System.Windows.Forms.RadioButton();
            this.rb_ch1 = new System.Windows.Forms.RadioButton();
            this.lb_unit = new System.Windows.Forms.Label();
            this.cmb_valutype = new System.Windows.Forms.ComboBox();
            this.btn_read = new System.Windows.Forms.Button();
            this.lb_value = new System.Windows.Forms.Label();
            this.btn_devb = new System.Windows.Forms.Button();
            this.btn_deva = new System.Windows.Forms.Button();
            this.btn_pub = new System.Windows.Forms.Button();
            this.btn_pua = new System.Windows.Forms.Button();
            this.btn_conndev = new System.Windows.Forms.Button();
            this.btn_findNext = new System.Windows.Forms.Button();
            this.tb_devName = new System.Windows.Forms.TextBox();
            this.btn_findRs = new System.Windows.Forms.Button();
            this.btn_openRM = new System.Windows.Forms.Button();
            this.gb_fluke = new System.Windows.Forms.GroupBox();
            this.lb_pinjieCount = new System.Windows.Forms.Label();
            this.btn_adc = new System.Windows.Forms.Button();
            this.lb_serialErrCount = new System.Windows.Forms.Label();
            this.lb_serialCount = new System.Windows.Forms.Label();
            this.lb_getPortUnit = new System.Windows.Forms.Label();
            this.btn_vdc = new System.Windows.Forms.Button();
            this.btn_vac = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_interval = new System.Windows.Forms.TextBox();
            this.lb_getPortValue = new System.Windows.Forms.Label();
            this.btn_startGetValue = new System.Windows.Forms.Button();
            this.cmb_sport = new System.Windows.Forms.ComboBox();
            this.btn_openPort = new System.Windows.Forms.Button();
            this.btn_mini = new System.Windows.Forms.Button();
            this.gb_tektronix.SuspendLayout();
            this.gb_fluke.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(955, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(22, 22);
            this.btn_close.TabIndex = 31;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // gb_tektronix
            // 
            this.gb_tektronix.Controls.Add(this.btn_ch2set);
            this.gb_tektronix.Controls.Add(this.btn_ch1set);
            this.gb_tektronix.Controls.Add(this.cb_bigchange);
            this.gb_tektronix.Controls.Add(this.rb_ch2);
            this.gb_tektronix.Controls.Add(this.rb_ch1);
            this.gb_tektronix.Controls.Add(this.lb_unit);
            this.gb_tektronix.Controls.Add(this.cmb_valutype);
            this.gb_tektronix.Controls.Add(this.btn_read);
            this.gb_tektronix.Controls.Add(this.lb_value);
            this.gb_tektronix.Controls.Add(this.btn_devb);
            this.gb_tektronix.Controls.Add(this.btn_deva);
            this.gb_tektronix.Controls.Add(this.btn_pub);
            this.gb_tektronix.Controls.Add(this.btn_pua);
            this.gb_tektronix.Controls.Add(this.btn_conndev);
            this.gb_tektronix.Controls.Add(this.btn_findNext);
            this.gb_tektronix.Controls.Add(this.tb_devName);
            this.gb_tektronix.Controls.Add(this.btn_findRs);
            this.gb_tektronix.Controls.Add(this.btn_openRM);
            this.gb_tektronix.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_tektronix.Location = new System.Drawing.Point(3, 2);
            this.gb_tektronix.Name = "gb_tektronix";
            this.gb_tektronix.Size = new System.Drawing.Size(565, 85);
            this.gb_tektronix.TabIndex = 33;
            this.gb_tektronix.TabStop = false;
            this.gb_tektronix.Text = "groupBox1";
            // 
            // btn_ch2set
            // 
            this.btn_ch2set.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ch2set.Location = new System.Drawing.Point(177, 60);
            this.btn_ch2set.Name = "btn_ch2set";
            this.btn_ch2set.Size = new System.Drawing.Size(90, 22);
            this.btn_ch2set.TabIndex = 36;
            this.btn_ch2set.Text = "CH2 ON/OFF";
            this.btn_ch2set.UseVisualStyleBackColor = true;
            this.btn_ch2set.Click += new System.EventHandler(this.btn_chSet_Click);
            // 
            // btn_ch1set
            // 
            this.btn_ch1set.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ch1set.Location = new System.Drawing.Point(84, 60);
            this.btn_ch1set.Name = "btn_ch1set";
            this.btn_ch1set.Size = new System.Drawing.Size(90, 22);
            this.btn_ch1set.TabIndex = 35;
            this.btn_ch1set.Text = "CH1 ON/OFF";
            this.btn_ch1set.UseVisualStyleBackColor = true;
            this.btn_ch1set.Click += new System.EventHandler(this.btn_chSet_Click);
            // 
            // cb_bigchange
            // 
            this.cb_bigchange.AutoSize = true;
            this.cb_bigchange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bigchange.Location = new System.Drawing.Point(429, 62);
            this.cb_bigchange.Name = "cb_bigchange";
            this.cb_bigchange.Size = new System.Drawing.Size(62, 19);
            this.cb_bigchange.TabIndex = 34;
            this.cb_bigchange.Text = "Coarse";
            this.cb_bigchange.UseVisualStyleBackColor = true;
            this.cb_bigchange.CheckedChanged += new System.EventHandler(this.cb_bigchange_CheckedChanged);
            // 
            // rb_ch2
            // 
            this.rb_ch2.AutoSize = true;
            this.rb_ch2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ch2.Location = new System.Drawing.Point(203, 39);
            this.rb_ch2.Name = "rb_ch2";
            this.rb_ch2.Size = new System.Drawing.Size(48, 19);
            this.rb_ch2.TabIndex = 33;
            this.rb_ch2.TabStop = true;
            this.rb_ch2.Text = "CH2";
            this.rb_ch2.UseVisualStyleBackColor = true;
            // 
            // rb_ch1
            // 
            this.rb_ch1.AutoSize = true;
            this.rb_ch1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_ch1.Location = new System.Drawing.Point(159, 39);
            this.rb_ch1.Name = "rb_ch1";
            this.rb_ch1.Size = new System.Drawing.Size(48, 19);
            this.rb_ch1.TabIndex = 32;
            this.rb_ch1.TabStop = true;
            this.rb_ch1.Text = "CH1";
            this.rb_ch1.UseVisualStyleBackColor = true;
            // 
            // lb_unit
            // 
            this.lb_unit.AutoSize = true;
            this.lb_unit.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_unit.Location = new System.Drawing.Point(344, 37);
            this.lb_unit.Name = "lb_unit";
            this.lb_unit.Size = new System.Drawing.Size(60, 22);
            this.lb_unit.TabIndex = 31;
            this.lb_unit.Text = "V rms";
            // 
            // cmb_valutype
            // 
            this.cmb_valutype.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_valutype.FormattingEnabled = true;
            this.cmb_valutype.Location = new System.Drawing.Point(254, 37);
            this.cmb_valutype.Name = "cmb_valutype";
            this.cmb_valutype.Size = new System.Drawing.Size(84, 23);
            this.cmb_valutype.TabIndex = 30;
            // 
            // btn_read
            // 
            this.btn_read.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_read.Location = new System.Drawing.Point(272, 60);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(101, 22);
            this.btn_read.TabIndex = 28;
            this.btn_read.Text = "Read Amplitude";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // lb_value
            // 
            this.lb_value.AutoSize = true;
            this.lb_value.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_value.Location = new System.Drawing.Point(344, 11);
            this.lb_value.Name = "lb_value";
            this.lb_value.Size = new System.Drawing.Size(76, 24);
            this.lb_value.TabIndex = 27;
            this.lb_value.Text = "5.0000";
            // 
            // btn_devb
            // 
            this.btn_devb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_devb.Location = new System.Drawing.Point(494, 36);
            this.btn_devb.Name = "btn_devb";
            this.btn_devb.Size = new System.Drawing.Size(67, 26);
            this.btn_devb.TabIndex = 26;
            this.btn_devb.Text = "-0.001";
            this.btn_devb.UseVisualStyleBackColor = true;
            this.btn_devb.Click += new System.EventHandler(this.btn_devb_Click);
            // 
            // btn_deva
            // 
            this.btn_deva.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_deva.Location = new System.Drawing.Point(427, 36);
            this.btn_deva.Name = "btn_deva";
            this.btn_deva.Size = new System.Drawing.Size(67, 26);
            this.btn_deva.TabIndex = 25;
            this.btn_deva.Text = "-0.01";
            this.btn_deva.UseVisualStyleBackColor = true;
            this.btn_deva.Click += new System.EventHandler(this.btn_deva_Click);
            // 
            // btn_pub
            // 
            this.btn_pub.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pub.Location = new System.Drawing.Point(494, 9);
            this.btn_pub.Name = "btn_pub";
            this.btn_pub.Size = new System.Drawing.Size(67, 26);
            this.btn_pub.TabIndex = 24;
            this.btn_pub.Text = "+0.001";
            this.btn_pub.UseVisualStyleBackColor = true;
            this.btn_pub.Click += new System.EventHandler(this.btn_pub_Click);
            // 
            // btn_pua
            // 
            this.btn_pua.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pua.Location = new System.Drawing.Point(427, 9);
            this.btn_pua.Name = "btn_pua";
            this.btn_pua.Size = new System.Drawing.Size(67, 26);
            this.btn_pua.TabIndex = 23;
            this.btn_pua.Text = "+0.01";
            this.btn_pua.UseVisualStyleBackColor = true;
            this.btn_pua.Click += new System.EventHandler(this.btn_pua_Click);
            // 
            // btn_conndev
            // 
            this.btn_conndev.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_conndev.Location = new System.Drawing.Point(4, 60);
            this.btn_conndev.Name = "btn_conndev";
            this.btn_conndev.Size = new System.Drawing.Size(78, 22);
            this.btn_conndev.TabIndex = 22;
            this.btn_conndev.Text = "Connect";
            this.btn_conndev.UseVisualStyleBackColor = true;
            this.btn_conndev.Click += new System.EventHandler(this.btn_conndev_Click);
            // 
            // btn_findNext
            // 
            this.btn_findNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_findNext.Location = new System.Drawing.Point(84, 38);
            this.btn_findNext.Name = "btn_findNext";
            this.btn_findNext.Size = new System.Drawing.Size(68, 22);
            this.btn_findNext.TabIndex = 21;
            this.btn_findNext.Text = "Switch";
            this.btn_findNext.UseVisualStyleBackColor = true;
            this.btn_findNext.Click += new System.EventHandler(this.btn_findNext_Click);
            // 
            // tb_devName
            // 
            this.tb_devName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_devName.Location = new System.Drawing.Point(85, 14);
            this.tb_devName.Name = "tb_devName";
            this.tb_devName.Size = new System.Drawing.Size(253, 23);
            this.tb_devName.TabIndex = 20;
            // 
            // btn_findRs
            // 
            this.btn_findRs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_findRs.Location = new System.Drawing.Point(4, 37);
            this.btn_findRs.Name = "btn_findRs";
            this.btn_findRs.Size = new System.Drawing.Size(78, 22);
            this.btn_findRs.TabIndex = 19;
            this.btn_findRs.Text = "Search";
            this.btn_findRs.UseVisualStyleBackColor = true;
            this.btn_findRs.Click += new System.EventHandler(this.btn_findRs_Click);
            // 
            // btn_openRM
            // 
            this.btn_openRM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_openRM.Location = new System.Drawing.Point(4, 14);
            this.btn_openRM.Name = "btn_openRM";
            this.btn_openRM.Size = new System.Drawing.Size(78, 22);
            this.btn_openRM.TabIndex = 18;
            this.btn_openRM.Text = "OpenTekVISA";
            this.btn_openRM.UseVisualStyleBackColor = true;
            this.btn_openRM.Click += new System.EventHandler(this.btn_openRM_Click);
            // 
            // gb_fluke
            // 
            this.gb_fluke.Controls.Add(this.lb_pinjieCount);
            this.gb_fluke.Controls.Add(this.btn_adc);
            this.gb_fluke.Controls.Add(this.lb_serialErrCount);
            this.gb_fluke.Controls.Add(this.lb_serialCount);
            this.gb_fluke.Controls.Add(this.lb_getPortUnit);
            this.gb_fluke.Controls.Add(this.btn_vdc);
            this.gb_fluke.Controls.Add(this.btn_vac);
            this.gb_fluke.Controls.Add(this.checkBox1);
            this.gb_fluke.Controls.Add(this.label3);
            this.gb_fluke.Controls.Add(this.tb_interval);
            this.gb_fluke.Controls.Add(this.lb_getPortValue);
            this.gb_fluke.Controls.Add(this.btn_startGetValue);
            this.gb_fluke.Controls.Add(this.cmb_sport);
            this.gb_fluke.Controls.Add(this.btn_openPort);
            this.gb_fluke.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_fluke.Location = new System.Drawing.Point(570, 2);
            this.gb_fluke.Name = "gb_fluke";
            this.gb_fluke.Size = new System.Drawing.Size(376, 85);
            this.gb_fluke.TabIndex = 34;
            this.gb_fluke.TabStop = false;
            this.gb_fluke.Text = "groupBox2";
            // 
            // lb_pinjieCount
            // 
            this.lb_pinjieCount.AutoSize = true;
            this.lb_pinjieCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_pinjieCount.Location = new System.Drawing.Point(249, 64);
            this.lb_pinjieCount.Name = "lb_pinjieCount";
            this.lb_pinjieCount.Size = new System.Drawing.Size(38, 15);
            this.lb_pinjieCount.TabIndex = 46;
            this.lb_pinjieCount.Text = "label1";
            // 
            // btn_adc
            // 
            this.btn_adc.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_adc.Location = new System.Drawing.Point(309, 56);
            this.btn_adc.Name = "btn_adc";
            this.btn_adc.Size = new System.Drawing.Size(65, 24);
            this.btn_adc.TabIndex = 45;
            this.btn_adc.Text = "A_DC";
            this.btn_adc.UseVisualStyleBackColor = true;
            this.btn_adc.Click += new System.EventHandler(this.btn_adc_Click);
            // 
            // lb_serialErrCount
            // 
            this.lb_serialErrCount.AutoSize = true;
            this.lb_serialErrCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_serialErrCount.Location = new System.Drawing.Point(185, 64);
            this.lb_serialErrCount.Name = "lb_serialErrCount";
            this.lb_serialErrCount.Size = new System.Drawing.Size(38, 15);
            this.lb_serialErrCount.TabIndex = 44;
            this.lb_serialErrCount.Text = "label1";
            // 
            // lb_serialCount
            // 
            this.lb_serialCount.AutoSize = true;
            this.lb_serialCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_serialCount.Location = new System.Drawing.Point(130, 64);
            this.lb_serialCount.Name = "lb_serialCount";
            this.lb_serialCount.Size = new System.Drawing.Size(38, 15);
            this.lb_serialCount.TabIndex = 43;
            this.lb_serialCount.Text = "label1";
            // 
            // lb_getPortUnit
            // 
            this.lb_getPortUnit.AutoSize = true;
            this.lb_getPortUnit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_getPortUnit.Location = new System.Drawing.Point(179, 37);
            this.lb_getPortUnit.Name = "lb_getPortUnit";
            this.lb_getPortUnit.Size = new System.Drawing.Size(53, 24);
            this.lb_getPortUnit.TabIndex = 42;
            this.lb_getPortUnit.Text = "VDC";
            // 
            // btn_vdc
            // 
            this.btn_vdc.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_vdc.Location = new System.Drawing.Point(309, 8);
            this.btn_vdc.Name = "btn_vdc";
            this.btn_vdc.Size = new System.Drawing.Size(65, 24);
            this.btn_vdc.TabIndex = 41;
            this.btn_vdc.Text = "V_DC";
            this.btn_vdc.UseVisualStyleBackColor = true;
            this.btn_vdc.Click += new System.EventHandler(this.btn_vdc_Click);
            // 
            // btn_vac
            // 
            this.btn_vac.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_vac.Location = new System.Drawing.Point(309, 32);
            this.btn_vac.Name = "btn_vac";
            this.btn_vac.Size = new System.Drawing.Size(65, 24);
            this.btn_vac.TabIndex = 40;
            this.btn_vac.Text = "V_AC";
            this.btn_vac.UseVisualStyleBackColor = true;
            this.btn_vac.Click += new System.EventHandler(this.btn_vac_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(7, 62);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(130, 19);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Text = "开启自动反馈控制";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "ms";
            // 
            // tb_interval
            // 
            this.tb_interval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_interval.Location = new System.Drawing.Point(7, 38);
            this.tb_interval.Name = "tb_interval";
            this.tb_interval.Size = new System.Drawing.Size(62, 23);
            this.tb_interval.TabIndex = 37;
            // 
            // lb_getPortValue
            // 
            this.lb_getPortValue.AutoSize = true;
            this.lb_getPortValue.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_getPortValue.Location = new System.Drawing.Point(179, 11);
            this.lb_getPortValue.Name = "lb_getPortValue";
            this.lb_getPortValue.Size = new System.Drawing.Size(76, 24);
            this.lb_getPortValue.TabIndex = 36;
            this.lb_getPortValue.Text = "5.0000";
            // 
            // btn_startGetValue
            // 
            this.btn_startGetValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startGetValue.Location = new System.Drawing.Point(98, 37);
            this.btn_startGetValue.Name = "btn_startGetValue";
            this.btn_startGetValue.Size = new System.Drawing.Size(77, 23);
            this.btn_startGetValue.TabIndex = 35;
            this.btn_startGetValue.Text = "Read Value";
            this.btn_startGetValue.UseVisualStyleBackColor = true;
            this.btn_startGetValue.Click += new System.EventHandler(this.btn_startGetValue_Click);
            // 
            // cmb_sport
            // 
            this.cmb_sport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_sport.FormattingEnabled = true;
            this.cmb_sport.Location = new System.Drawing.Point(7, 14);
            this.cmb_sport.Name = "cmb_sport";
            this.cmb_sport.Size = new System.Drawing.Size(76, 23);
            this.cmb_sport.TabIndex = 34;
            // 
            // btn_openPort
            // 
            this.btn_openPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_openPort.Location = new System.Drawing.Point(89, 13);
            this.btn_openPort.Name = "btn_openPort";
            this.btn_openPort.Size = new System.Drawing.Size(86, 23);
            this.btn_openPort.TabIndex = 33;
            this.btn_openPort.Text = "Open SPort";
            this.btn_openPort.UseVisualStyleBackColor = true;
            this.btn_openPort.Click += new System.EventHandler(this.btn_openPort_Click);
            // 
            // btn_mini
            // 
            this.btn_mini.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_mini.Location = new System.Drawing.Point(955, 31);
            this.btn_mini.Name = "btn_mini";
            this.btn_mini.Size = new System.Drawing.Size(22, 22);
            this.btn_mini.TabIndex = 35;
            this.btn_mini.Text = "--";
            this.btn_mini.UseVisualStyleBackColor = true;
            this.btn_mini.Click += new System.EventHandler(this.btn_mini_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 89);
            this.Controls.Add(this.btn_mini);
            this.Controls.Add(this.gb_fluke);
            this.Controls.Add(this.gb_tektronix);
            this.Controls.Add(this.btn_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.gb_tektronix.ResumeLayout(false);
            this.gb_tektronix.PerformLayout();
            this.gb_fluke.ResumeLayout(false);
            this.gb_fluke.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox gb_tektronix;
        private System.Windows.Forms.RadioButton rb_ch2;
        private System.Windows.Forms.RadioButton rb_ch1;
        private System.Windows.Forms.Label lb_unit;
        private System.Windows.Forms.ComboBox cmb_valutype;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.Label lb_value;
        private System.Windows.Forms.Button btn_devb;
        private System.Windows.Forms.Button btn_deva;
        private System.Windows.Forms.Button btn_pub;
        private System.Windows.Forms.Button btn_pua;
        private System.Windows.Forms.Button btn_conndev;
        private System.Windows.Forms.Button btn_findNext;
        private System.Windows.Forms.TextBox tb_devName;
        private System.Windows.Forms.Button btn_findRs;
        private System.Windows.Forms.Button btn_openRM;
        private System.Windows.Forms.GroupBox gb_fluke;
        private System.Windows.Forms.Label lb_serialCount;
        private System.Windows.Forms.Label lb_getPortUnit;
        private System.Windows.Forms.Button btn_vdc;
        private System.Windows.Forms.Button btn_vac;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_interval;
        private System.Windows.Forms.Label lb_getPortValue;
        private System.Windows.Forms.Button btn_startGetValue;
        private System.Windows.Forms.ComboBox cmb_sport;
        private System.Windows.Forms.Button btn_openPort;
        private System.Windows.Forms.Button btn_mini;
        private System.Windows.Forms.CheckBox cb_bigchange;
        private System.Windows.Forms.Label lb_serialErrCount;
        private System.Windows.Forms.Button btn_ch1set;
        private System.Windows.Forms.Button btn_ch2set;
        private System.Windows.Forms.Button btn_adc;
        private System.Windows.Forms.Label lb_pinjieCount;
    }
}

