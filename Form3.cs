using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace test
{
    public partial class Form3 : Form
    {
        public int ReaderLineCount=0;
        long[] AllowTime_IPass = { 320, 50, 200, 50, 115, 50 };
        long[] AllowTime_ICash = { 115, 50, 315, 50, 390, 50 };
        long[] AllowTime_EasyCard = { 230, 50, 385, 50, 165, 50 };
        long[] AllowTime_YHDP = { 230, 50, 385, 50, 165, 50 };//遠鑫未檢測
        int Times, StandardTimes;
        string path = "TestFile.csv";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            initparameter();
            SetTBTxt();
        }

        private void AllowFindTime_IPass_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1 parentForm = (Form1)this.Owner;
            parentForm.MsgFromChild = "Msg from child-form success";     
            

        }

        private void initparameter() 
        {

            long[] AllowTime_IPass = new long[6];
            long[] AllowTime_EasyCard = new long[6];
            long[] AllowTime_ICash = new long[6];
            string line;
            using (StreamReader sr = File.OpenText(path))
            {
                while ((line = sr.ReadLine()) != null)//讀取檔案參數
                {
                    ReaderLineCount++;
                    string[] sArray = Regex.Split(line, ",");
                    if (ReaderLineCount == 1)
                    {
                        StandardTimes = Int32.Parse(sArray[1]);
                        Times = StandardTimes;
                    }
                    if (ReaderLineCount == 2)
                    { continue; }
                    if (ReaderLineCount == 3) //IPass尋讀寫容許值
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            AllowTime_IPass[i - 1] = Int64.Parse(sArray[i]);

                        }
                    }
                    if (ReaderLineCount == 4)//EasyCard尋讀寫容許值
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            AllowTime_EasyCard[i - 1] = Int64.Parse(sArray[i]);
                        }
                    }
                    if (ReaderLineCount == 5)//ICash尋讀寫容許值
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            AllowTime_ICash[i - 1] = Int64.Parse(sArray[i]);
                        }
                    }

                }
            }
        }

        private void SetTBTxt() 
        {
            AllowFindTime_textBox.Text = Convert.ToString(AllowTime_IPass[0]);
            AllowFindTimeRange_textBox.Text = Convert.ToString(AllowTime_IPass[1]);
            AllowReadTime_textBox.Text = Convert.ToString(AllowTime_IPass[2]);
            AllowReadTimeRange_textBox.Text = Convert.ToString(AllowTime_IPass[3]);
            AllowWriteTime_textBox.Text = Convert.ToString(AllowTime_IPass[4]);
            AllowWriteTimeRange_textBox.Text = Convert.ToString(AllowTime_IPass[5]);
            Times_TB.Text = Convert.ToString(Times);
        }

    }
}
