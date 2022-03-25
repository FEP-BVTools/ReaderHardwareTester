namespace test
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AutoSensingTimer = new System.Windows.Forms.Timer(this.components);
            this.OFind_Lab = new System.Windows.Forms.Label();
            this.ORead_Lab = new System.Windows.Forms.Label();
            this.OWrite_Lab = new System.Windows.Forms.Label();
            this.ms_Lab1 = new System.Windows.Forms.Label();
            this.ms_Lab2 = new System.Windows.Forms.Label();
            this.ms_Lab3 = new System.Windows.Forms.Label();
            this.AFind_Lab = new System.Windows.Forms.Label();
            this.ARead_Lab = new System.Windows.Forms.Label();
            this.AWrite_Lab = new System.Windows.Forms.Label();
            this.ms_Lab5 = new System.Windows.Forms.Label();
            this.ms_Lab6 = new System.Windows.Forms.Label();
            this.ms_Lab7 = new System.Windows.Forms.Label();
            this.OTotal_Lab = new System.Windows.Forms.Label();
            this.ms_Lab4 = new System.Windows.Forms.Label();
            this.ATotal_Lab = new System.Windows.Forms.Label();
            this.ms_Lab8 = new System.Windows.Forms.Label();
            this.AReset_BT = new System.Windows.Forms.Button();
            this.Start_BT = new System.Windows.Forms.Button();
            this.OReset_BT = new System.Windows.Forms.Button();
            this.ComPort_Lab = new System.Windows.Forms.Label();
            this.ComPort_CB = new System.Windows.Forms.ComboBox();
            this.RS232 = new System.IO.Ports.SerialPort(this.components);
            this.OFTime_Lab = new System.Windows.Forms.Label();
            this.ORTime_Lab = new System.Windows.Forms.Label();
            this.OWTime_Lab = new System.Windows.Forms.Label();
            this.OTTime_Lab = new System.Windows.Forms.Label();
            this.AFTime_Lab = new System.Windows.Forms.Label();
            this.ARTime_Lab = new System.Windows.Forms.Label();
            this.AWTime_Lab = new System.Windows.Forms.Label();
            this.ATTime_Lab = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.Clear_BT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.HSFTime_Lab = new System.Windows.Forms.Label();
            this.HFFTime_Lab = new System.Windows.Forms.Label();
            this.HSRTime_Lab = new System.Windows.Forms.Label();
            this.HSTTime_Lab = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.HFRTime_Lab = new System.Windows.Forms.Label();
            this.HSWTime_Lab = new System.Windows.Forms.Label();
            this.HFTTime_Lab = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.HFWTime_Lab = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.選項ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baudRate設置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Baud115200 = new System.Windows.Forms.ToolStripMenuItem();
            this.Baud57600 = new System.Windows.Forms.ToolStripMenuItem();
            this.eCCSIS2115200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查卡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模式變更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自動測試ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.靠卡測試ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deBug視窗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.TestSuccessLab = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TestFailLab = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ModeLab = new System.Windows.Forms.Label();
            this.ReaderReset = new System.Windows.Forms.Button();
            this.SAMSlotTest_Btn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Start_BT_Right = new System.Windows.Forms.Button();
            this.Start_BT_Back = new System.Windows.Forms.Button();
            this.Start_BT_Forward = new System.Windows.Forms.Button();
            this.Start_BT_Left = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.DVID_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SAMExSlotTest_Btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.AutoSensingSw_Btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoSensingTimer
            // 
            this.AutoSensingTimer.Interval = 1000;
            this.AutoSensingTimer.Tick += new System.EventHandler(this.AutoSensingTimer_Tick);
            // 
            // OFind_Lab
            // 
            this.OFind_Lab.AutoSize = true;
            this.OFind_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.OFind_Lab.Location = new System.Drawing.Point(7, 38);
            this.OFind_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OFind_Lab.Name = "OFind_Lab";
            this.OFind_Lab.Size = new System.Drawing.Size(56, 16);
            this.OFind_Lab.TabIndex = 4;
            this.OFind_Lab.Text = "尋卡：";
            // 
            // ORead_Lab
            // 
            this.ORead_Lab.AutoSize = true;
            this.ORead_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.ORead_Lab.Location = new System.Drawing.Point(7, 66);
            this.ORead_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ORead_Lab.Name = "ORead_Lab";
            this.ORead_Lab.Size = new System.Drawing.Size(56, 16);
            this.ORead_Lab.TabIndex = 5;
            this.ORead_Lab.Text = "讀卡：";
            // 
            // OWrite_Lab
            // 
            this.OWrite_Lab.AutoSize = true;
            this.OWrite_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.OWrite_Lab.Location = new System.Drawing.Point(7, 96);
            this.OWrite_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OWrite_Lab.Name = "OWrite_Lab";
            this.OWrite_Lab.Size = new System.Drawing.Size(56, 16);
            this.OWrite_Lab.TabIndex = 6;
            this.OWrite_Lab.Text = "寫卡：";
            // 
            // ms_Lab1
            // 
            this.ms_Lab1.AutoSize = true;
            this.ms_Lab1.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab1.Location = new System.Drawing.Point(101, 38);
            this.ms_Lab1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab1.Name = "ms_Lab1";
            this.ms_Lab1.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab1.TabIndex = 10;
            this.ms_Lab1.Text = "毫秒";
            // 
            // ms_Lab2
            // 
            this.ms_Lab2.AutoSize = true;
            this.ms_Lab2.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab2.Location = new System.Drawing.Point(101, 66);
            this.ms_Lab2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab2.Name = "ms_Lab2";
            this.ms_Lab2.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab2.TabIndex = 11;
            this.ms_Lab2.Text = "毫秒";
            // 
            // ms_Lab3
            // 
            this.ms_Lab3.AutoSize = true;
            this.ms_Lab3.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab3.Location = new System.Drawing.Point(101, 96);
            this.ms_Lab3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab3.Name = "ms_Lab3";
            this.ms_Lab3.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab3.TabIndex = 12;
            this.ms_Lab3.Text = "毫秒";
            // 
            // AFind_Lab
            // 
            this.AFind_Lab.AutoSize = true;
            this.AFind_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.AFind_Lab.Location = new System.Drawing.Point(7, 56);
            this.AFind_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AFind_Lab.Name = "AFind_Lab";
            this.AFind_Lab.Size = new System.Drawing.Size(56, 16);
            this.AFind_Lab.TabIndex = 14;
            this.AFind_Lab.Text = "尋卡：";
            // 
            // ARead_Lab
            // 
            this.ARead_Lab.AutoSize = true;
            this.ARead_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.ARead_Lab.Location = new System.Drawing.Point(7, 86);
            this.ARead_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ARead_Lab.Name = "ARead_Lab";
            this.ARead_Lab.Size = new System.Drawing.Size(56, 16);
            this.ARead_Lab.TabIndex = 15;
            this.ARead_Lab.Text = "讀卡：";
            // 
            // AWrite_Lab
            // 
            this.AWrite_Lab.AutoSize = true;
            this.AWrite_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.AWrite_Lab.Location = new System.Drawing.Point(7, 113);
            this.AWrite_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AWrite_Lab.Name = "AWrite_Lab";
            this.AWrite_Lab.Size = new System.Drawing.Size(56, 16);
            this.AWrite_Lab.TabIndex = 16;
            this.AWrite_Lab.Text = "寫卡：";
            // 
            // ms_Lab5
            // 
            this.ms_Lab5.AutoSize = true;
            this.ms_Lab5.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab5.Location = new System.Drawing.Point(219, 56);
            this.ms_Lab5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab5.Name = "ms_Lab5";
            this.ms_Lab5.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab5.TabIndex = 20;
            this.ms_Lab5.Text = "毫秒";
            // 
            // ms_Lab6
            // 
            this.ms_Lab6.AutoSize = true;
            this.ms_Lab6.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab6.Location = new System.Drawing.Point(219, 86);
            this.ms_Lab6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab6.Name = "ms_Lab6";
            this.ms_Lab6.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab6.TabIndex = 21;
            this.ms_Lab6.Text = "毫秒";
            // 
            // ms_Lab7
            // 
            this.ms_Lab7.AutoSize = true;
            this.ms_Lab7.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab7.Location = new System.Drawing.Point(219, 113);
            this.ms_Lab7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab7.Name = "ms_Lab7";
            this.ms_Lab7.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab7.TabIndex = 22;
            this.ms_Lab7.Text = "毫秒";
            // 
            // OTotal_Lab
            // 
            this.OTotal_Lab.AutoSize = true;
            this.OTotal_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.OTotal_Lab.Location = new System.Drawing.Point(7, 127);
            this.OTotal_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OTotal_Lab.Name = "OTotal_Lab";
            this.OTotal_Lab.Size = new System.Drawing.Size(56, 16);
            this.OTotal_Lab.TabIndex = 23;
            this.OTotal_Lab.Text = "總共：";
            this.OTotal_Lab.Click += new System.EventHandler(this.OTotal_Lab_Click);
            // 
            // ms_Lab4
            // 
            this.ms_Lab4.AutoSize = true;
            this.ms_Lab4.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab4.Location = new System.Drawing.Point(101, 127);
            this.ms_Lab4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab4.Name = "ms_Lab4";
            this.ms_Lab4.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab4.TabIndex = 24;
            this.ms_Lab4.Text = "毫秒";
            // 
            // ATotal_Lab
            // 
            this.ATotal_Lab.AutoSize = true;
            this.ATotal_Lab.Font = new System.Drawing.Font("新細明體", 12F);
            this.ATotal_Lab.Location = new System.Drawing.Point(8, 140);
            this.ATotal_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ATotal_Lab.Name = "ATotal_Lab";
            this.ATotal_Lab.Size = new System.Drawing.Size(56, 16);
            this.ATotal_Lab.TabIndex = 25;
            this.ATotal_Lab.Text = "總共：";
            // 
            // ms_Lab8
            // 
            this.ms_Lab8.AutoSize = true;
            this.ms_Lab8.Font = new System.Drawing.Font("新細明體", 12F);
            this.ms_Lab8.Location = new System.Drawing.Point(219, 140);
            this.ms_Lab8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ms_Lab8.Name = "ms_Lab8";
            this.ms_Lab8.Size = new System.Drawing.Size(40, 16);
            this.ms_Lab8.TabIndex = 26;
            this.ms_Lab8.Text = "毫秒";
            // 
            // AReset_BT
            // 
            this.AReset_BT.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AReset_BT.Location = new System.Drawing.Point(1033, 375);
            this.AReset_BT.Margin = new System.Windows.Forms.Padding(4);
            this.AReset_BT.Name = "AReset_BT";
            this.AReset_BT.Size = new System.Drawing.Size(112, 33);
            this.AReset_BT.TabIndex = 31;
            this.AReset_BT.Text = "平均重置";
            this.AReset_BT.UseVisualStyleBackColor = true;
            this.AReset_BT.Click += new System.EventHandler(this.AReset_BT_Click);
            // 
            // Start_BT
            // 
            this.Start_BT.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Start_BT.ForeColor = System.Drawing.Color.Red;
            this.Start_BT.Image = ((System.Drawing.Image)(resources.GetObject("Start_BT.Image")));
            this.Start_BT.Location = new System.Drawing.Point(71, 114);
            this.Start_BT.Margin = new System.Windows.Forms.Padding(4);
            this.Start_BT.Name = "Start_BT";
            this.Start_BT.Size = new System.Drawing.Size(232, 120);
            this.Start_BT.TabIndex = 33;
            this.Start_BT.UseVisualStyleBackColor = true;
            this.Start_BT.Click += new System.EventHandler(this.Start_BT_Click);
            // 
            // OReset_BT
            // 
            this.OReset_BT.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OReset_BT.Location = new System.Drawing.Point(819, 375);
            this.OReset_BT.Margin = new System.Windows.Forms.Padding(4);
            this.OReset_BT.Name = "OReset_BT";
            this.OReset_BT.Size = new System.Drawing.Size(112, 33);
            this.OReset_BT.TabIndex = 36;
            this.OReset_BT.Text = "單次重置";
            this.OReset_BT.UseVisualStyleBackColor = true;
            this.OReset_BT.Click += new System.EventHandler(this.OReset_BT_Click);
            // 
            // ComPort_Lab
            // 
            this.ComPort_Lab.AutoSize = true;
            this.ComPort_Lab.Location = new System.Drawing.Point(22, 50);
            this.ComPort_Lab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ComPort_Lab.Name = "ComPort_Lab";
            this.ComPort_Lab.Size = new System.Drawing.Size(79, 16);
            this.ComPort_Lab.TabIndex = 44;
            this.ComPort_Lab.Text = "ComPort：";
            // 
            // ComPort_CB
            // 
            this.ComPort_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComPort_CB.FormattingEnabled = true;
            this.ComPort_CB.Location = new System.Drawing.Point(108, 47);
            this.ComPort_CB.Name = "ComPort_CB";
            this.ComPort_CB.Size = new System.Drawing.Size(78, 24);
            this.ComPort_CB.TabIndex = 45;
            this.ComPort_CB.DropDown += new System.EventHandler(this.ComPort_CB_DropDown);
            this.ComPort_CB.SelectedIndexChanged += new System.EventHandler(this.ComPort_CB_SelectedIndexChanged);
            this.ComPort_CB.TextChanged += new System.EventHandler(this.ComPort_CB_TextChanged);
            // 
            // RS232
            // 
            this.RS232.BaudRate = 57600;
            // 
            // OFTime_Lab
            // 
            this.OFTime_Lab.AutoSize = true;
            this.OFTime_Lab.Location = new System.Drawing.Point(70, 38);
            this.OFTime_Lab.Name = "OFTime_Lab";
            this.OFTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.OFTime_Lab.TabIndex = 47;
            this.OFTime_Lab.Text = "0";
            // 
            // ORTime_Lab
            // 
            this.ORTime_Lab.AutoSize = true;
            this.ORTime_Lab.Location = new System.Drawing.Point(70, 66);
            this.ORTime_Lab.Name = "ORTime_Lab";
            this.ORTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.ORTime_Lab.TabIndex = 48;
            this.ORTime_Lab.Text = "0";
            // 
            // OWTime_Lab
            // 
            this.OWTime_Lab.AutoSize = true;
            this.OWTime_Lab.Location = new System.Drawing.Point(70, 96);
            this.OWTime_Lab.Name = "OWTime_Lab";
            this.OWTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.OWTime_Lab.TabIndex = 49;
            this.OWTime_Lab.Text = "0";
            // 
            // OTTime_Lab
            // 
            this.OTTime_Lab.AutoSize = true;
            this.OTTime_Lab.Location = new System.Drawing.Point(70, 127);
            this.OTTime_Lab.Name = "OTTime_Lab";
            this.OTTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.OTTime_Lab.TabIndex = 50;
            this.OTTime_Lab.Text = "0";
            // 
            // AFTime_Lab
            // 
            this.AFTime_Lab.AutoSize = true;
            this.AFTime_Lab.Location = new System.Drawing.Point(123, 56);
            this.AFTime_Lab.Name = "AFTime_Lab";
            this.AFTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.AFTime_Lab.TabIndex = 51;
            this.AFTime_Lab.Text = "0";
            // 
            // ARTime_Lab
            // 
            this.ARTime_Lab.AutoSize = true;
            this.ARTime_Lab.Location = new System.Drawing.Point(123, 86);
            this.ARTime_Lab.Name = "ARTime_Lab";
            this.ARTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.ARTime_Lab.TabIndex = 52;
            this.ARTime_Lab.Text = "0";
            // 
            // AWTime_Lab
            // 
            this.AWTime_Lab.AutoSize = true;
            this.AWTime_Lab.Location = new System.Drawing.Point(123, 113);
            this.AWTime_Lab.Name = "AWTime_Lab";
            this.AWTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.AWTime_Lab.TabIndex = 53;
            this.AWTime_Lab.Text = "0";
            // 
            // ATTime_Lab
            // 
            this.ATTime_Lab.AutoSize = true;
            this.ATTime_Lab.Location = new System.Drawing.Point(123, 140);
            this.ATTime_Lab.Name = "ATTime_Lab";
            this.ATTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.ATTime_Lab.TabIndex = 54;
            this.ATTime_Lab.Text = "0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.richTextBox1.Location = new System.Drawing.Point(497, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(214, 588);
            this.richTextBox1.TabIndex = 55;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.RichTextBox1_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 5000;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(790, 47);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(413, 321);
            this.richTextBox2.TabIndex = 56;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.RichTextBox2_TextChanged);
            // 
            // Clear_BT
            // 
            this.Clear_BT.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Clear_BT.Location = new System.Drawing.Point(298, 44);
            this.Clear_BT.Margin = new System.Windows.Forms.Padding(4);
            this.Clear_BT.Name = "Clear_BT";
            this.Clear_BT.Size = new System.Drawing.Size(97, 29);
            this.Clear_BT.TabIndex = 57;
            this.Clear_BT.Text = "清除訊息";
            this.Clear_BT.UseVisualStyleBackColor = true;
            this.Clear_BT.Click += new System.EventHandler(this.Clear_BT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OTotal_Lab);
            this.groupBox1.Controls.Add(this.OTTime_Lab);
            this.groupBox1.Controls.Add(this.ms_Lab4);
            this.groupBox1.Controls.Add(this.OWrite_Lab);
            this.groupBox1.Controls.Add(this.OWTime_Lab);
            this.groupBox1.Controls.Add(this.ms_Lab3);
            this.groupBox1.Controls.Add(this.ms_Lab2);
            this.groupBox1.Controls.Add(this.ORTime_Lab);
            this.groupBox1.Controls.Add(this.ORead_Lab);
            this.groupBox1.Controls.Add(this.OFTime_Lab);
            this.groupBox1.Controls.Add(this.OFind_Lab);
            this.groupBox1.Controls.Add(this.ms_Lab1);
            this.groupBox1.Location = new System.Drawing.Point(790, 425);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 169);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "單次";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.HSFTime_Lab);
            this.groupBox2.Controls.Add(this.HFFTime_Lab);
            this.groupBox2.Controls.Add(this.HSRTime_Lab);
            this.groupBox2.Controls.Add(this.HSTTime_Lab);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.HFRTime_Lab);
            this.groupBox2.Controls.Add(this.HSWTime_Lab);
            this.groupBox2.Controls.Add(this.HFTTime_Lab);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.AFind_Lab);
            this.groupBox2.Controls.Add(this.HFWTime_Lab);
            this.groupBox2.Controls.Add(this.AFTime_Lab);
            this.groupBox2.Controls.Add(this.ms_Lab5);
            this.groupBox2.Controls.Add(this.ARead_Lab);
            this.groupBox2.Controls.Add(this.ARTime_Lab);
            this.groupBox2.Controls.Add(this.ms_Lab6);
            this.groupBox2.Controls.Add(this.ATTime_Lab);
            this.groupBox2.Controls.Add(this.AWrite_Lab);
            this.groupBox2.Controls.Add(this.AWTime_Lab);
            this.groupBox2.Controls.Add(this.ms_Lab7);
            this.groupBox2.Controls.Add(this.ATotal_Lab);
            this.groupBox2.Controls.Add(this.ms_Lab8);
            this.groupBox2.Location = new System.Drawing.Point(955, 425);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 169);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "平均";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F);
            this.label10.Location = new System.Drawing.Point(164, 23);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "最慢";
            // 
            // HSFTime_Lab
            // 
            this.HSFTime_Lab.AutoSize = true;
            this.HSFTime_Lab.Location = new System.Drawing.Point(174, 56);
            this.HSFTime_Lab.Name = "HSFTime_Lab";
            this.HSFTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HSFTime_Lab.TabIndex = 51;
            this.HSFTime_Lab.Text = "0";
            // 
            // HFFTime_Lab
            // 
            this.HFFTime_Lab.AutoSize = true;
            this.HFFTime_Lab.Location = new System.Drawing.Point(79, 56);
            this.HFFTime_Lab.Name = "HFFTime_Lab";
            this.HFFTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HFFTime_Lab.TabIndex = 51;
            this.HFFTime_Lab.Text = "0";
            // 
            // HSRTime_Lab
            // 
            this.HSRTime_Lab.AutoSize = true;
            this.HSRTime_Lab.Location = new System.Drawing.Point(174, 86);
            this.HSRTime_Lab.Name = "HSRTime_Lab";
            this.HSRTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HSRTime_Lab.TabIndex = 52;
            this.HSRTime_Lab.Text = "0";
            // 
            // HSTTime_Lab
            // 
            this.HSTTime_Lab.AutoSize = true;
            this.HSTTime_Lab.Location = new System.Drawing.Point(174, 140);
            this.HSTTime_Lab.Name = "HSTTime_Lab";
            this.HSTTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HSTTime_Lab.TabIndex = 54;
            this.HSTTime_Lab.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F);
            this.label8.Location = new System.Drawing.Point(112, 23);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "平均";
            // 
            // HFRTime_Lab
            // 
            this.HFRTime_Lab.AutoSize = true;
            this.HFRTime_Lab.Location = new System.Drawing.Point(79, 86);
            this.HFRTime_Lab.Name = "HFRTime_Lab";
            this.HFRTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HFRTime_Lab.TabIndex = 52;
            this.HFRTime_Lab.Text = "0";
            // 
            // HSWTime_Lab
            // 
            this.HSWTime_Lab.AutoSize = true;
            this.HSWTime_Lab.Location = new System.Drawing.Point(174, 113);
            this.HSWTime_Lab.Name = "HSWTime_Lab";
            this.HSWTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HSWTime_Lab.TabIndex = 53;
            this.HSWTime_Lab.Text = "0";
            // 
            // HFTTime_Lab
            // 
            this.HFTTime_Lab.AutoSize = true;
            this.HFTTime_Lab.Location = new System.Drawing.Point(79, 140);
            this.HFTTime_Lab.Name = "HFTTime_Lab";
            this.HFTTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HFTTime_Lab.TabIndex = 54;
            this.HFTTime_Lab.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.Location = new System.Drawing.Point(65, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "最快";
            // 
            // HFWTime_Lab
            // 
            this.HFWTime_Lab.AutoSize = true;
            this.HFWTime_Lab.Location = new System.Drawing.Point(79, 113);
            this.HFWTime_Lab.Name = "HFWTime_Lab";
            this.HFWTime_Lab.Size = new System.Drawing.Size(16, 16);
            this.HFWTime_Lab.TabIndex = 53;
            this.HFWTime_Lab.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設置ToolStripMenuItem,
            this.工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 24);
            this.menuStrip1.TabIndex = 61;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設置ToolStripMenuItem
            // 
            this.設置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選項ToolStripMenuItem,
            this.baudRate設置ToolStripMenuItem});
            this.設置ToolStripMenuItem.Name = "設置ToolStripMenuItem";
            this.設置ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.設置ToolStripMenuItem.Text = "設置";
            // 
            // 選項ToolStripMenuItem
            // 
            this.選項ToolStripMenuItem.Name = "選項ToolStripMenuItem";
            this.選項ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.選項ToolStripMenuItem.Text = "容許值參數";
            this.選項ToolStripMenuItem.Visible = false;
            this.選項ToolStripMenuItem.Click += new System.EventHandler(this.選項ToolStripMenuItem_Click);
            // 
            // baudRate設置ToolStripMenuItem
            // 
            this.baudRate設置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Baud115200,
            this.Baud57600,
            this.eCCSIS2115200ToolStripMenuItem,
            this.eCCToolStripMenuItem});
            this.baudRate設置ToolStripMenuItem.Name = "baudRate設置ToolStripMenuItem";
            this.baudRate設置ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.baudRate設置ToolStripMenuItem.Text = "卡機通訊速率設置";
            // 
            // Baud115200
            // 
            this.Baud115200.CheckOnClick = true;
            this.Baud115200.Name = "Baud115200";
            this.Baud115200.Size = new System.Drawing.Size(173, 22);
            this.Baud115200.Text = "檢測用";
            this.Baud115200.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // Baud57600
            // 
            this.Baud57600.Name = "Baud57600";
            this.Baud57600.Size = new System.Drawing.Size(173, 22);
            this.Baud57600.Text = "ECC-LOG3";
            this.Baud57600.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // eCCSIS2115200ToolStripMenuItem
            // 
            this.eCCSIS2115200ToolStripMenuItem.Name = "eCCSIS2115200ToolStripMenuItem";
            this.eCCSIS2115200ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.eCCSIS2115200ToolStripMenuItem.Text = "ECC-SIS2-115200";
            this.eCCSIS2115200ToolStripMenuItem.Click += new System.EventHandler(this.eCCSIS2115200ToolStripMenuItem_Click);
            // 
            // eCCToolStripMenuItem
            // 
            this.eCCToolStripMenuItem.Name = "eCCToolStripMenuItem";
            this.eCCToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.eCCToolStripMenuItem.Text = "ECC-SIS2-57600";
            this.eCCToolStripMenuItem.Click += new System.EventHandler(this.eCCToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查卡ToolStripMenuItem,
            this.模式變更ToolStripMenuItem,
            this.deBug視窗ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // 查卡ToolStripMenuItem
            // 
            this.查卡ToolStripMenuItem.Enabled = false;
            this.查卡ToolStripMenuItem.Name = "查卡ToolStripMenuItem";
            this.查卡ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.查卡ToolStripMenuItem.Text = "查卡";
            this.查卡ToolStripMenuItem.Click += new System.EventHandler(this.查卡ToolStripMenuItem_Click);
            // 
            // 模式變更ToolStripMenuItem
            // 
            this.模式變更ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自動測試ToolStripMenuItem,
            this.靠卡測試ToolStripMenuItem});
            this.模式變更ToolStripMenuItem.Name = "模式變更ToolStripMenuItem";
            this.模式變更ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.模式變更ToolStripMenuItem.Text = "模式變更";
            // 
            // 自動測試ToolStripMenuItem
            // 
            this.自動測試ToolStripMenuItem.Name = "自動測試ToolStripMenuItem";
            this.自動測試ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.自動測試ToolStripMenuItem.Text = "自動測試";
            this.自動測試ToolStripMenuItem.Click += new System.EventHandler(this.自動測試ToolStripMenuItem_Click);
            // 
            // 靠卡測試ToolStripMenuItem
            // 
            this.靠卡測試ToolStripMenuItem.Name = "靠卡測試ToolStripMenuItem";
            this.靠卡測試ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.靠卡測試ToolStripMenuItem.Text = "靠卡測試";
            this.靠卡測試ToolStripMenuItem.Click += new System.EventHandler(this.靠卡測試ToolStripMenuItem_Click);
            // 
            // deBug視窗ToolStripMenuItem
            // 
            this.deBug視窗ToolStripMenuItem.Name = "deBug視窗ToolStripMenuItem";
            this.deBug視窗ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.deBug視窗ToolStripMenuItem.Text = "DeBug視窗";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "測試成功 / 失敗：";
            this.label1.Click += new System.EventHandler(this.OTotal_Lab_Click);
            // 
            // TestSuccessLab
            // 
            this.TestSuccessLab.AutoSize = true;
            this.TestSuccessLab.Location = new System.Drawing.Point(133, 23);
            this.TestSuccessLab.Name = "TestSuccessLab";
            this.TestSuccessLab.Size = new System.Drawing.Size(16, 16);
            this.TestSuccessLab.TabIndex = 50;
            this.TestSuccessLab.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(156, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "/";
            // 
            // TestFailLab
            // 
            this.TestFailLab.AutoSize = true;
            this.TestFailLab.Location = new System.Drawing.Point(175, 23);
            this.TestFailLab.Name = "TestFailLab";
            this.TestFailLab.Size = new System.Drawing.Size(16, 16);
            this.TestFailLab.TabIndex = 50;
            this.TestFailLab.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Mode：";
            // 
            // ModeLab
            // 
            this.ModeLab.AutoSize = true;
            this.ModeLab.Location = new System.Drawing.Point(66, 23);
            this.ModeLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ModeLab.Name = "ModeLab";
            this.ModeLab.Size = new System.Drawing.Size(72, 16);
            this.ModeLab.TabIndex = 44;
            this.ModeLab.Text = "自動測試";
            // 
            // ReaderReset
            // 
            this.ReaderReset.Enabled = false;
            this.ReaderReset.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReaderReset.Location = new System.Drawing.Point(193, 43);
            this.ReaderReset.Margin = new System.Windows.Forms.Padding(4);
            this.ReaderReset.Name = "ReaderReset";
            this.ReaderReset.Size = new System.Drawing.Size(97, 30);
            this.ReaderReset.TabIndex = 57;
            this.ReaderReset.Text = "卡機重啟";
            this.ReaderReset.UseVisualStyleBackColor = true;
            this.ReaderReset.Click += new System.EventHandler(this.ReaderReset_Click);
            // 
            // SAMSlotTest_Btn
            // 
            this.SAMSlotTest_Btn.Enabled = false;
            this.SAMSlotTest_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SAMSlotTest_Btn.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SAMSlotTest_Btn.Location = new System.Drawing.Point(193, 99);
            this.SAMSlotTest_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.SAMSlotTest_Btn.Name = "SAMSlotTest_Btn";
            this.SAMSlotTest_Btn.Size = new System.Drawing.Size(121, 33);
            this.SAMSlotTest_Btn.TabIndex = 33;
            this.SAMSlotTest_Btn.Text = "SAM底層槽測試";
            this.SAMSlotTest_Btn.UseVisualStyleBackColor = true;
            this.SAMSlotTest_Btn.Click += new System.EventHandler(this.SAMSlotTest_Btn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox3.BackgroundImage")));
            this.groupBox3.Controls.Add(this.Start_BT_Right);
            this.groupBox3.Controls.Add(this.Start_BT_Back);
            this.groupBox3.Controls.Add(this.Start_BT_Forward);
            this.groupBox3.Controls.Add(this.Start_BT_Left);
            this.groupBox3.Controls.Add(this.Start_BT);
            this.groupBox3.Location = new System.Drawing.Point(25, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 350);
            this.groupBox3.TabIndex = 62;
            this.groupBox3.TabStop = false;
            // 
            // Start_BT_Right
            // 
            this.Start_BT_Right.Enabled = false;
            this.Start_BT_Right.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Start_BT_Right.ForeColor = System.Drawing.Color.Red;
            this.Start_BT_Right.Image = ((System.Drawing.Image)(resources.GetObject("Start_BT_Right.Image")));
            this.Start_BT_Right.Location = new System.Drawing.Point(231, 60);
            this.Start_BT_Right.Margin = new System.Windows.Forms.Padding(4);
            this.Start_BT_Right.Name = "Start_BT_Right";
            this.Start_BT_Right.Size = new System.Drawing.Size(140, 227);
            this.Start_BT_Right.TabIndex = 33;
            this.Start_BT_Right.UseVisualStyleBackColor = true;
            this.Start_BT_Right.Visible = false;
            this.Start_BT_Right.Click += new System.EventHandler(this.Start_BT_Click);
            // 
            // Start_BT_Back
            // 
            this.Start_BT_Back.Enabled = false;
            this.Start_BT_Back.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Start_BT_Back.ForeColor = System.Drawing.Color.Red;
            this.Start_BT_Back.Image = ((System.Drawing.Image)(resources.GetObject("Start_BT_Back.Image")));
            this.Start_BT_Back.Location = new System.Drawing.Point(71, 219);
            this.Start_BT_Back.Margin = new System.Windows.Forms.Padding(4);
            this.Start_BT_Back.Name = "Start_BT_Back";
            this.Start_BT_Back.Size = new System.Drawing.Size(232, 121);
            this.Start_BT_Back.TabIndex = 33;
            this.Start_BT_Back.UseVisualStyleBackColor = true;
            this.Start_BT_Back.Visible = false;
            this.Start_BT_Back.Click += new System.EventHandler(this.Start_BT_Click);
            // 
            // Start_BT_Forward
            // 
            this.Start_BT_Forward.Enabled = false;
            this.Start_BT_Forward.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_BT_Forward.ForeColor = System.Drawing.Color.Red;
            this.Start_BT_Forward.Image = ((System.Drawing.Image)(resources.GetObject("Start_BT_Forward.Image")));
            this.Start_BT_Forward.Location = new System.Drawing.Point(71, 11);
            this.Start_BT_Forward.Margin = new System.Windows.Forms.Padding(4);
            this.Start_BT_Forward.Name = "Start_BT_Forward";
            this.Start_BT_Forward.Size = new System.Drawing.Size(232, 141);
            this.Start_BT_Forward.TabIndex = 33;
            this.Start_BT_Forward.UseVisualStyleBackColor = true;
            this.Start_BT_Forward.Visible = false;
            this.Start_BT_Forward.Click += new System.EventHandler(this.Start_BT_Click);
            // 
            // Start_BT_Left
            // 
            this.Start_BT_Left.Enabled = false;
            this.Start_BT_Left.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Start_BT_Left.ForeColor = System.Drawing.Color.Red;
            this.Start_BT_Left.Image = ((System.Drawing.Image)(resources.GetObject("Start_BT_Left.Image")));
            this.Start_BT_Left.Location = new System.Drawing.Point(7, 60);
            this.Start_BT_Left.Margin = new System.Windows.Forms.Padding(4);
            this.Start_BT_Left.Name = "Start_BT_Left";
            this.Start_BT_Left.Size = new System.Drawing.Size(146, 227);
            this.Start_BT_Left.TabIndex = 33;
            this.Start_BT_Left.UseVisualStyleBackColor = true;
            this.Start_BT_Left.Visible = false;
            this.Start_BT_Left.Click += new System.EventHandler(this.Start_BT_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TestSuccessLab);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.TestFailLab);
            this.groupBox4.Location = new System.Drawing.Point(205, 565);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(206, 50);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.ModeLab);
            this.groupBox5.Location = new System.Drawing.Point(40, 565);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(159, 50);
            this.groupBox5.TabIndex = 64;
            this.groupBox5.TabStop = false;
            // 
            // DVID_TB
            // 
            this.DVID_TB.Location = new System.Drawing.Point(108, 104);
            this.DVID_TB.Name = "DVID_TB";
            this.DVID_TB.Size = new System.Drawing.Size(78, 27);
            this.DVID_TB.TabIndex = 65;
            this.DVID_TB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DVID_TB_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "DeviceID:";
            // 
            // SAMExSlotTest_Btn
            // 
            this.SAMExSlotTest_Btn.Enabled = false;
            this.SAMExSlotTest_Btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SAMExSlotTest_Btn.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SAMExSlotTest_Btn.Location = new System.Drawing.Point(322, 98);
            this.SAMExSlotTest_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.SAMExSlotTest_Btn.Name = "SAMExSlotTest_Btn";
            this.SAMExSlotTest_Btn.Size = new System.Drawing.Size(128, 33);
            this.SAMExSlotTest_Btn.TabIndex = 33;
            this.SAMExSlotTest_Btn.Text = "SAM擴充槽測試";
            this.SAMExSlotTest_Btn.UseVisualStyleBackColor = true;
            this.SAMExSlotTest_Btn.Click += new System.EventHandler(this.SAMSlotTest_Btn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(402, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 26);
            this.button1.TabIndex = 66;
            this.button1.Text = "中斷測試";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // AutoSensingSw_Btn
            // 
            this.AutoSensingSw_Btn.AllowDrop = true;
            this.AutoSensingSw_Btn.Location = new System.Drawing.Point(172, 535);
            this.AutoSensingSw_Btn.Name = "AutoSensingSw_Btn";
            this.AutoSensingSw_Btn.Size = new System.Drawing.Size(118, 33);
            this.AutoSensingSw_Btn.TabIndex = 67;
            this.AutoSensingSw_Btn.Text = "倒數器開關(Open)";
            this.AutoSensingSw_Btn.UseVisualStyleBackColor = true;
            this.AutoSensingSw_Btn.Click += new System.EventHandler(this.AutoSensingSw_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(723, 636);
            this.Controls.Add(this.AutoSensingSw_Btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.DVID_TB);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ReaderReset);
            this.Controls.Add(this.Clear_BT);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.ComPort_CB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComPort_Lab);
            this.Controls.Add(this.OReset_BT);
            this.Controls.Add(this.SAMExSlotTest_Btn);
            this.Controls.Add(this.SAMSlotTest_Btn);
            this.Controls.Add(this.AReset_BT);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "TS-2000_硬體測試程式";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label OFind_Lab;
        private System.Windows.Forms.Label ORead_Lab;
        private System.Windows.Forms.Label OWrite_Lab;
        private System.Windows.Forms.Label ms_Lab1;
        private System.Windows.Forms.Label ms_Lab2;
        private System.Windows.Forms.Label ms_Lab3;
        private System.Windows.Forms.Label AFind_Lab;
        private System.Windows.Forms.Label ARead_Lab;
        private System.Windows.Forms.Label AWrite_Lab;
        private System.Windows.Forms.Label ms_Lab5;
        private System.Windows.Forms.Label ms_Lab6;
        private System.Windows.Forms.Label ms_Lab7;
        private System.Windows.Forms.Label OTotal_Lab;
        private System.Windows.Forms.Label ms_Lab4;
        private System.Windows.Forms.Label ATotal_Lab;
        private System.Windows.Forms.Label ms_Lab8;
        private System.Windows.Forms.Button AReset_BT;
        private System.Windows.Forms.Button Start_BT;
        private System.Windows.Forms.Button OReset_BT;
        private System.Windows.Forms.Label ComPort_Lab;
        private System.Windows.Forms.ComboBox ComPort_CB;
        private System.IO.Ports.SerialPort RS232;
        private System.Windows.Forms.Label OFTime_Lab;
        private System.Windows.Forms.Label ORTime_Lab;
        private System.Windows.Forms.Label OWTime_Lab;
        private System.Windows.Forms.Label OTTime_Lab;
        private System.Windows.Forms.Label AFTime_Lab;
        private System.Windows.Forms.Label ARTime_Lab;
        private System.Windows.Forms.Label AWTime_Lab;
        private System.Windows.Forms.Label ATTime_Lab;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button Clear_BT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 選項ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TestSuccessLab;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label TestFailLab;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查卡ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模式變更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自動測試ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 靠卡測試ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deBug視窗ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ModeLab;
        private System.Windows.Forms.Button ReaderReset;
        private System.Windows.Forms.Button SAMSlotTest_Btn;
        private System.Windows.Forms.Label HFFTime_Lab;
        private System.Windows.Forms.Label HFRTime_Lab;
        private System.Windows.Forms.Label HFTTime_Lab;
        private System.Windows.Forms.Label HFWTime_Lab;
        private System.Windows.Forms.Label HSFTime_Lab;
        private System.Windows.Forms.Label HSRTime_Lab;
        private System.Windows.Forms.Label HSTTime_Lab;
        private System.Windows.Forms.Label HSWTime_Lab;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem baudRate設置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Baud115200;
        private System.Windows.Forms.ToolStripMenuItem Baud57600;
        private System.Windows.Forms.ToolStripMenuItem eCCSIS2115200ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eCCToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Start_BT_Forward;
        private System.Windows.Forms.Button Start_BT_Back;
        private System.Windows.Forms.Button Start_BT_Right;
        private System.Windows.Forms.Button Start_BT_Left;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox DVID_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SAMExSlotTest_Btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button AutoSensingSw_Btn;
        private System.Windows.Forms.Timer AutoSensingTimer;
    }
}

