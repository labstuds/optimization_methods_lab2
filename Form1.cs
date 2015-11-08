using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LoggerEvsSpace;
namespace optimizationLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initializeEnvironment();
            takeTask21();
            // Установить начальные значения входных параметров в методе адаптивного случайного поиска
            nudAlpha.Value = 1.618M;
            nudBeta.Value = 0.618M;
            nudR.Value = 0.001M;
            nudM.Value = 3M;
            nudN.Value = 100M;
        }

        private void initializeEnvironment()
        {
            LoggerEvs.messageCame += appendLogMessage;
        }

        public void appendLogMessage(String message)
        {
            logTextBox.AppendText(message);
            logTextBox.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void radioTask21_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTask21.Checked)
            {
                radioTask211.Checked = false;
                takeTask21();
            }
        }

        private void radioTask211_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTask211.Checked)
            {
                radioTask21.Checked = false;
                takeTask211();
            }
        }

        private void takeTask21()
        {
            Lab.task = new Task21();
            LoggerEvs.writeLog("С этого момента расчитывается задача #2.1.");
        }

        private void takeTask211()
        {
            Lab.task = new Task211();
            LoggerEvs.writeLog("С этого момента расчитывается задача #2.1.1.");
        }

        private void hdCalculateButton_Click(object sender, EventArgs e)
        {
            Vector2 result = HDMethod.calculate((double)hdX1Box.Value,(double)hdX2Box.Value,(double)hdH1Box.Value,(double)hdH1Box.Value,(double)hdEpsBox.Value);
            hdAnswerLabel.Text = "[" + result.ToString() + "], значение в этой точке = " + string.Format("{0:f4}",Lab.task.formula(result));
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            //  Метод адаптивного поиска
            Vector2 x_0 = new Vector2(Convert.ToDouble(nudX.Value), Convert.ToDouble(nudY.Value));
            Vector2 answer = AdaptiveSearchMethod.findMinimum(x_0, Convert.ToDouble(nudAlpha.Value), Convert.ToDouble(nudBeta.Value), Convert.ToInt32(nudT0.Value), Convert.ToInt32(nudM.Value), Convert.ToDouble(nudR.Value), Convert.ToInt32(nudN.Value));
            tbXAnswer.Text = answer.ToString();
            double funcValue = Lab.task.formula(answer);
            tbFAnswer.Text = string.Format("{0:f4}", funcValue);
        }
    }
}
