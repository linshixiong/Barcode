using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Emdoor.Barcode
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            this.init();
        }

        private void init()
        {

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                string printer = PrinterSettings.InstalledPrinters[i];
                comboBoxPrinter.Items.Add(printer);
                if (printer.Equals(Settings.Default.Printer))
                {
                    comboBoxPrinter.SelectedIndex = i;
                }
            }

            //printer 
            comboBoxPrintSpeed.SelectedIndex = Settings.Default.PrintSpeed - 1;
            comboBoxPrintDensity.SelectedIndex = Settings.Default.PrintDensity;
            comboBoxVertical.SelectedIndex = Settings.Default.Vertical;
            comboBoxOffset.SelectedIndex = Settings.Default.Offset;
            comboBoxSensor.SelectedIndex = Settings.Default.Sensor;


            //bar code
            comboBox_B_Type.SelectedIndex = Settings.Default.B_Type;
            textBox_B_Height.Text = Settings.Default.B_Height.ToString();
            comboBox_B_Readable.SelectedIndex = Settings.Default.B_Readable;
            comboBox_B_Rotation.SelectedIndex = Settings.Default.B_Rotation;
            textBoxNarrow.Text = Settings.Default.B_Narrow.ToString();
            textBoxWide.Text = Settings.Default.B_Wide.ToString();

            //text
            textBox_TH.Text = Settings.Default.T_H.ToString();
            comboBox_T_S.SelectedIndex = Settings.Default.T_S;
            comboBox_T_R.SelectedIndex = Settings.Default.T_R;
            comboBox_T_L.SelectedIndex = Settings.Default.T_L;
            comboBox_T_F.SelectedIndex = Settings.Default.T_F;

            //layout
            textBox_X.Text = Settings.Default.T_X.ToString();
            textBox_Y.Text = Settings.Default.T_Y.ToString();
            textBox_Gap1.Text = Settings.Default.Gap1.ToString();
            textBox_Gap2.Text = Settings.Default.Gap2.ToString();

            //label
            textBox_label_gap.Text = Settings.Default.LabelGap.ToString();
            textBox_label_column.Text = Settings.Default.LabelColumn.ToString();
            textBoxLabelHeight.Text = Settings.Default.LabelHeight.ToString();
            textBoxLabelWidth.Text = Settings.Default.LabelWidth.ToString();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void button_save_config_Click(object sender, EventArgs e)
        {
            try
            {
                saveConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "错误的输入参数", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }


        private void saveConfig()
        {
            Settings.Default.Printer = comboBoxPrinter.SelectedItem.ToString();
            Settings.Default.PrintSpeed = comboBoxPrintSpeed.SelectedIndex + 1;
            Settings.Default.PrintDensity = comboBoxPrintDensity.SelectedIndex;
            Settings.Default.Vertical = comboBoxVertical.SelectedIndex;
            Settings.Default.Offset = comboBoxOffset.SelectedIndex;
            Settings.Default.Sensor = comboBoxSensor.SelectedIndex;


            Settings.Default.B_Height = Convert.ToInt32(textBox_B_Height.Text);
            Settings.Default.B_Narrow = Convert.ToInt32(textBoxNarrow.Text);
            Settings.Default.B_Wide = Convert.ToInt32(textBoxWide.Text);
            Settings.Default.B_Type = comboBox_B_Type.SelectedIndex;
            Settings.Default.B_Readable = comboBox_B_Readable.SelectedIndex;
            Settings.Default.B_Rotation = comboBox_B_Rotation.SelectedIndex;

            Settings.Default.T_H = Convert.ToInt32(textBox_TH.Text);
            Settings.Default.T_S = comboBox_T_S.SelectedIndex;
            Settings.Default.T_R = comboBox_T_R.SelectedIndex;
            Settings.Default.T_L = comboBox_T_L.SelectedIndex;
            Settings.Default.T_F = comboBox_T_F.SelectedIndex;


            Settings.Default.T_X = Convert.ToInt32(textBox_X.Text);
            Settings.Default.T_Y = Convert.ToInt32(textBox_Y.Text);
            Settings.Default.Gap1 = Convert.ToInt32(textBox_Gap1.Text);
            Settings.Default.Gap2 = Convert.ToInt32(textBox_Gap2.Text);

            Settings.Default.LabelHeight = Convert.ToSingle(textBoxLabelHeight.Text);
            Settings.Default.LabelWidth = Convert.ToSingle(textBoxLabelWidth.Text);
            Settings.Default.LabelGap = Convert.ToSingle(textBox_label_gap.Text);
            Settings.Default.LabelColumn = Convert.ToInt32(textBox_label_column.Text);

            Settings.Default.Save();
            this.Close();
        }


    }
}
