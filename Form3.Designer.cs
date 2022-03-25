namespace test
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AllowFindTime_textBox = new System.Windows.Forms.TextBox();
            this.AllowReadTime_textBox = new System.Windows.Forms.TextBox();
            this.AllowWriteTime_textBox = new System.Windows.Forms.TextBox();
            this.AllowFindTimeRange_textBox = new System.Windows.Forms.TextBox();
            this.AllowReadTimeRange_textBox = new System.Windows.Forms.TextBox();
            this.AllowWriteTimeRange_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Times_TB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "尋卡";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "讀卡";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "寫卡";
            // 
            // AllowFindTime_textBox
            // 
            this.AllowFindTime_textBox.Location = new System.Drawing.Point(74, 40);
            this.AllowFindTime_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowFindTime_textBox.Name = "AllowFindTime_textBox";
            this.AllowFindTime_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowFindTime_textBox.TabIndex = 2;
            this.AllowFindTime_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // AllowReadTime_textBox
            // 
            this.AllowReadTime_textBox.Location = new System.Drawing.Point(74, 74);
            this.AllowReadTime_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowReadTime_textBox.Name = "AllowReadTime_textBox";
            this.AllowReadTime_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowReadTime_textBox.TabIndex = 2;
            this.AllowReadTime_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // AllowWriteTime_textBox
            // 
            this.AllowWriteTime_textBox.Location = new System.Drawing.Point(74, 108);
            this.AllowWriteTime_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowWriteTime_textBox.Name = "AllowWriteTime_textBox";
            this.AllowWriteTime_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowWriteTime_textBox.TabIndex = 2;
            this.AllowWriteTime_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // AllowFindTimeRange_textBox
            // 
            this.AllowFindTimeRange_textBox.Location = new System.Drawing.Point(170, 40);
            this.AllowFindTimeRange_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowFindTimeRange_textBox.Name = "AllowFindTimeRange_textBox";
            this.AllowFindTimeRange_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowFindTimeRange_textBox.TabIndex = 2;
            this.AllowFindTimeRange_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // AllowReadTimeRange_textBox
            // 
            this.AllowReadTimeRange_textBox.Location = new System.Drawing.Point(170, 74);
            this.AllowReadTimeRange_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowReadTimeRange_textBox.Name = "AllowReadTimeRange_textBox";
            this.AllowReadTimeRange_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowReadTimeRange_textBox.TabIndex = 2;
            this.AllowReadTimeRange_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // AllowWriteTimeRange_textBox
            // 
            this.AllowWriteTimeRange_textBox.Location = new System.Drawing.Point(170, 110);
            this.AllowWriteTimeRange_textBox.Margin = new System.Windows.Forms.Padding(2);
            this.AllowWriteTimeRange_textBox.Name = "AllowWriteTimeRange_textBox";
            this.AllowWriteTimeRange_textBox.Size = new System.Drawing.Size(76, 22);
            this.AllowWriteTimeRange_textBox.TabIndex = 2;
            this.AllowWriteTimeRange_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(154, 42);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "+";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(154, 79);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "+";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(154, 113);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "+";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.AllowWriteTimeRange_textBox);
            this.groupBox1.Controls.Add(this.Times_TB);
            this.groupBox1.Controls.Add(this.AllowWriteTime_textBox);
            this.groupBox1.Controls.Add(this.AllowReadTimeRange_textBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AllowFindTimeRange_textBox);
            this.groupBox1.Controls.Add(this.AllowReadTime_textBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AllowFindTime_textBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(28, 51);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(259, 188);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "參數調整";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "(ms)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "(ms)";
            // 
            // Times_TB
            // 
            this.Times_TB.Location = new System.Drawing.Point(74, 147);
            this.Times_TB.Margin = new System.Windows.Forms.Padding(2);
            this.Times_TB.MaxLength = 4;
            this.Times_TB.Name = "Times_TB";
            this.Times_TB.Size = new System.Drawing.Size(76, 22);
            this.Times_TB.TabIndex = 2;
            this.Times_TB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowFindTime_IPass_textBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 150);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "測試次數";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 243);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 288);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form3";
            this.Text = "容許值調整";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AllowFindTime_textBox;
        private System.Windows.Forms.TextBox AllowReadTime_textBox;
        private System.Windows.Forms.TextBox AllowWriteTime_textBox;
        private System.Windows.Forms.TextBox AllowFindTimeRange_textBox;
        private System.Windows.Forms.TextBox AllowReadTimeRange_textBox;
        private System.Windows.Forms.TextBox AllowWriteTimeRange_textBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Times_TB;
        private System.Windows.Forms.Label label6;
    }
}