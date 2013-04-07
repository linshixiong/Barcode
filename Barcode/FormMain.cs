using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;


namespace Emdoor.Barcode
{
    public partial class FormMain : Form
    {

        private string imeiTitle;
        private string imei;
        private string snTitle;
        private string sn;
        private string wifiMACTitle;
        private string wifiMAC;
        protected delegate void UpdateUIDelegate();
        private int labelColumnCount = 3;
        private float labelGap = 2.5f;
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "串号文件(*.sn)|*.sn|所有文件(*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fileDialog.FileName;
                FileInfo file = new FileInfo(fileDialog.FileName);
                FileWatcherStrat(file.Directory.FullName, "*.sn");

                GetCodeFromFile();
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPrintCount.Text))
            {
                MessageBox.Show(this, "请输入正确的打印数量！", "错误的输入参数", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int count = Convert.ToInt32(textBoxPrintCount.Text);

            if (count <= 0)
            {
                MessageBox.Show(this, "请输入正确的打印数量！", "错误的输入参数", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                AddLogItem(imei, sn, wifiMAC);
                labelColumnCount = Settings.Default.LabelColumn;
                labelGap = Settings.Default.LabelGap;
                int row_count = count / labelColumnCount;
                int remainder = count % labelColumnCount;



                for (int i = 0; i < row_count; i++)
                {
                    print(labelColumnCount);
                }
                if (remainder > 0)
                {
                    print(remainder);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            this.GetCodeFromFile();
            //this.labelMsg.Text = "sn文件已更新！";
        }

        private void GetCodeFromFile()
        {
            SerialReader sr = new SerialReader(txtFileName.Text);
            this.imei = sr.GetIMEI();
            this.sn = sr.GetSN();
            this.wifiMAC = sr.GetWifiMAC();

            if (string.IsNullOrEmpty(imei) || string.IsNullOrEmpty(sn))
            {
                this.btnPrint.Enabled = false;
                this.labelMsg.Text = "无效的串号文件";
                return;
            }
            else
            {
                this.btnPrint.Enabled = true;
                this.labelMsg.Text = "";
            }


            textOutput.Text = "";
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(imei))
            {
                imeiTitle = string.Format("IMEI:{0}", imei);
                sb.AppendLine(imeiTitle);
            }

            if (!string.IsNullOrEmpty(sn))
            {
                snTitle = string.Format("SN:{0}", sn);
                sb.AppendLine(snTitle);
            }
            if (!string.IsNullOrEmpty(wifiMAC))
            {
                wifiMACTitle = string.Format("WIFI MAC:{0}", wifiMAC);
                sb.AppendLine(wifiMACTitle);
            }

            textOutput.Text = sb.ToString();


        }



        private void AddLogItem(string imei, string sn, string wifi_mac)
        {
            string logFileName = string.Format("{0}\\result.csv", System.Environment.CurrentDirectory);
            bool fileExists = File.Exists(logFileName);
            StreamWriter sr = new StreamWriter(logFileName, true, Encoding.UTF8);
            if (!fileExists)
            {
                sr.WriteLine("IMEI号,序列号,WIFI MAC,创建时间");

            }
            //sb_data.append("\"\t" + rs.getString(i) + "\"");
            string line = string.Format("\"\t{0}\",{1},{2},\"\t{3}\"", imei, sn, wifi_mac, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sr.WriteLine(line);
            sr.Close();

        }


        private void FileWatcherStrat(string path, string filter)
        {

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = filter;
            watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            watcher.Deleted += new FileSystemEventHandler(OnProcess);
            watcher.Renamed += new RenamedEventHandler(OnProcess);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;
        }

        private void OnProcess(object source, FileSystemEventArgs e)
        {

            this.Invoke(new UpdateUIDelegate(UpdateUI));

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FormConfig fc = new FormConfig();
            fc.ShowDialog();
        }




        private void print(int count)
        {
            if (count > labelColumnCount)
            {
                return;
            }

            TSCLib.openport(Settings.Default.Printer);


            //打印机参数
            string l_width = (Settings.Default.LabelWidth * labelColumnCount + labelGap * (labelColumnCount - 1)).ToString();
            string l_height = (Settings.Default.LabelHeight + labelGap).ToString();
            string p_speed = Settings.Default.PrintSpeed.ToString();
            string p_density = Settings.Default.PrintDensity.ToString();
            string p_sensor = Settings.Default.Sensor.ToString();
            string p_vertical = Settings.Default.Vertical.ToString();
            string p_offset = Settings.Default.Offset.ToString();

            //设置打印机

            TSCLib.setup(l_width, l_height, p_speed, p_density, p_sensor, p_vertical, p_offset);
            TSCLib.clearbuffer();



            for (int index = 0; index < count; index++)
            {
                //字体样式
                int t_x = Settings.Default.T_X;
                int t_y = Settings.Default.T_Y;
                int t_height = Settings.Default.T_H;
                int t_rotation = Settings.Default.T_R;
                int t_font_style = Settings.Default.T_S;
                int t_under_line = Settings.Default.T_L;

                //条码样式
                string b_y = (t_y + t_height + Settings.Default.Gap1).ToString();
                string b_height = Settings.Default.B_Height.ToString();
                string b_readable = Settings.Default.B_Readable.ToString();
                string b_rotation = Settings.Default.B_Rotation.ToString();
                string b_narrow = Settings.Default.B_Narrow.ToString();
                string b_wide = Settings.Default.B_Wide.ToString();

                if (index > 0)
                {
                    t_x = 30 * 12 * index + 2 * 12 * index + t_x;
                }
                //打印SN字符    
                TSCLib.windowsfont(t_x, t_y, t_height, 0, t_font_style, t_under_line, "Times new Roman", "HCL S/N:" + sn);


                //打印SN条码
                TSCLib.barcode(t_x.ToString(), b_y, "128", b_height, b_readable, "0", b_narrow, b_wide, sn);

                //打印IMEI字符
                t_y = Convert.ToInt32(b_y) + Settings.Default.B_Height + Settings.Default.Gap2;
                TSCLib.windowsfont(t_x, t_y, t_height, 0, t_font_style, t_under_line, "Times new Roman", "IMEI:" + imei);
                b_y = (t_y + t_height + Settings.Default.Gap1).ToString();
               TSCLib.barcode(t_x.ToString(), b_y, "128", b_height, b_readable, "0", b_narrow, b_wide, imei);
            }


            TSCLib.printlabel("1", "1");
            TSCLib.closeport();
            TSCLib.formfeed();
        }


        private void print(bool result)
        {

            TSCLib.openport("TSC TTP-344M Plus");
            //设置打印机
            TSCLib.setup("32", "17.5", "4", "14", "0", "0", "0");
            TSCLib.clearbuffer();
            //第一行文字    
            TSCLib.windowsfont(60, 40, 28, 0, 0, 0, "Times new Roman", "S/N:ABCDEF123456");
            //第二行二维码样式
            TSCLib.barcode("60", "70", "128", "40", "0", "0", "2", "2", "ABCDEF123456");
            //第三行文字
            TSCLib.windowsfont(60, 120, 28, 0, 0, 0, "Times new Roman", "IMEI:123456789012345");
            //第四行文字
            TSCLib.barcode("60", "150", "128", "40", "0", "0", "2", "2", "123456789012345");
            TSCLib.printlabel("1", "1");
            TSCLib.closeport();
        }

        private void textBoxPrintCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }
}
