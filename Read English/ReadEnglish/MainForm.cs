using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.IO.Compression;
using System.IO;

namespace ReadEnglish
{
    public partial class MainForm : MetroForm
    {
        public Monitor myMonitor = new Monitor();
        public Dictionary myDictionary = new Dictionary();
        public string transcription = "en";
        public MainForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            /*int percent = myMonitor.percentText(redactorRichTextBox.Text);
            int countSym = myMonitor.countSymbol(redactorRichTextBox.Text);
            int graphPercent = percent < 100 ? percent : 100;

            graphicChart.Series[0].Points[0].YValues[0] = graphPercent;
            graphicChart.Series[0].Points[1].YValues[0] = 100 - graphPercent;

            graphicLabel.Text = percent.ToString() + '%';
            graphCaptionLabel2.Text = countSym.ToString() + " / " + myMonitor.normText.ToString();
            graphicChart.Refresh();*/

            renewalMonitorLabel();

        }

        private void renewalMonitorLabel()
        {
            int percent = myMonitor.percentText(redactorRichTextBox.Text);
            int countSym = myMonitor.countSymbol(redactorRichTextBox.Text);
            int graphPercent = percent < 100 ? percent : 100;

            graphicChart.Series[0].Points[0].YValues[0] = graphPercent;
            graphicChart.Series[0].Points[1].YValues[0] = 100 - graphPercent;
            graphicLabel.Text = percent.ToString() + '%';
            graphicChart.Refresh();

            //// слова
            graphCaptionLabel2.Text = countSym.ToString() + " / " + myMonitor.textRate.ToString();
            textBox5.Text = countSym.ToString() + " / " + myMonitor.textRate.ToString();
            //// знаки
            textBox2.Text = myDictionary.entryCount().ToString() + " / " + myMonitor.wordsRate.ToString();
            label3.Text = myDictionary.entryCount().ToString() + " / " + myMonitor.wordsRate.ToString();
            //// имя файла
        }

        private void redactorRichTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void переводToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string word = redactorRichTextBox.SelectedText;
            if (word != "")
            {
                myDictionary.entryAdd(word);
                if (myDictionary.isEmpty())
                {
                    return;
                }
                Dictionary.dictionaryEntry en = myDictionary.entryGet(myDictionary.entryCount() - 1);
                dictionaryGrid.Rows.Add(en.word, transcription == "en" ? en.transcriptionBritish : en.transcriptionAmerican, en.translation);
                //label3.Text = myDictionary.entryCount().ToString() + " / " + myMonitor.normWords.ToString();
                renewalMonitorLabel();
                metroTabControl1.SelectTab(1);
            }            
        }

        private void alignmentLeftButton_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void alignmentCenterButton_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void alignmentRightButton_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Ваш принтер не поддерживается.", "Ошибка печати", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //redactorRichTextBox.SaveFile("text.rtf");
            saveAsDialogForm saveForm = new saveAsDialogForm(this);
            //saveForm.Owner = this;
            saveForm.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //float size = float.Parse(comboBox2.SelectedItem.ToString());
            //redactorRichTextBox.SelectionFont = new Font(comboBox1.SelectedItem.ToString(), size);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            float size = (float)(numericUpDown1.Value);
            if (comboBox1.SelectedItem == null)
            {
                comboBox1.SelectedItem = comboBox1.Items[1];
            }

            redactorRichTextBox.SelectionFont = new Font(comboBox1.SelectedItem.ToString(), size);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            float size = (float)(numericUpDown1.Value);
            if (comboBox1.SelectedItem == null)
            {
                comboBox1.SelectedItem = comboBox1.Items[1];
            }
            redactorRichTextBox.SelectionFont = new Font(comboBox1.SelectedItem.ToString(), size);
        }

        private void dictionaryGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dictionaryContextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }

        private void удалитьЭлементToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dictionaryGrid.SelectedRows[0].Index;
            try
            {
                dictionaryGrid.Rows.RemoveAt(index);
                myDictionary.entryDelete(index);
                //label3.Text = myDictionary.entryCount().ToString() + " / " + myMonitor.normWords.ToString();
                renewalMonitorLabel();
            }
            catch
            {
                MessageBox.Show("Невозможно удаление строки.", "Ошибка словаря", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.Paste();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redactorRichTextBox.Cut();
        }

        private void redactorRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (redactorRichTextBox.SelectionFont != null)
            {
                comboBox1.Text = redactorRichTextBox.SelectionFont.Name;
                numericUpDown1.Value = (int) redactorRichTextBox.SelectionFont.Size;
            }
            else
            {
                //comboBox1.Text = "Arial";
                //numericUpDown1.Value = 14;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            myDictionary.save("dic.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dictionaryGrid.Rows.Clear();
            myDictionary.open("dic.txt");

            int count = myDictionary.entryCount();
            for (int i = 0; i < count; i++)
            {
                Dictionary.dictionaryEntry en = myDictionary.entryGet(i);
                dictionaryGrid.Rows.Add(en.word, transcription == "en" ? en.transcriptionBritish : en.transcriptionAmerican, en.translation);
            }
        }

        private void openFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //string fileName = "";
            string filePath = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            string fileName = Path.GetFileName(openFileDialog1.FileName);

            textBox1.Text = fileName;

            ZipFile.ExtractToDirectory(openFileDialog1.FileName, filePath);
            myDictionary.open(filePath + "\\" + "dictionary.txt");
            myMonitor.open(filePath + "\\" + "monitor.txt");
            redactorRichTextBox.LoadFile(filePath + "\\" + "text.rtf");

            Directory.Delete(filePath, true);

            numericUpDown2.Value = myMonitor.textRate;
            dictionaryGrid.Rows.Clear();
            int count = myDictionary.entryCount();
            for (int i = 0; i < count; i++)
            {
                Dictionary.dictionaryEntry en = myDictionary.entryGet(i);
                dictionaryGrid.Rows.Add(en.word, transcription == "en" ? en.transcriptionBritish : en.transcriptionAmerican, en.translation);
                //label3.Text = myDictionary.entryCount().ToString() + " / " + myMonitor.normWords.ToString();
                renewalMonitorLabel();
            }
        }

        private void saveFile(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string fileName = Path.GetFileName(saveFileDialog1.FileName);
            string filePath = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);

            textBox1.Text = fileName;

            Directory.CreateDirectory(filePath);

            myDictionary.save(filePath + "\\" + "dictionary.txt");
            redactorRichTextBox.SaveFile(filePath + "\\" + "text.rtf");
            myMonitor.save(filePath + "\\" + "monitor.txt");

            ZipFile.CreateFromDirectory(filePath, saveFileDialog1.FileName, CompressionLevel.NoCompression, false);
            Directory.Delete(filePath, true);            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            myMonitor.textRate = (int) numericUpDown2.Value;
            myMonitor.wordsRate = (int)(myMonitor.textRate / 200);
            renewalMonitorLabel();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dictionaryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //MetroMessageBox.Show(this, "dddd", "ffrt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            transcription = "am";
            button5.Enabled = false;
            button6.Enabled = true;

            dictionaryGrid.Rows.Clear();
            int count = myDictionary.entryCount();
            for (int i = 0; i < count; i++)
            {
                Dictionary.dictionaryEntry en = myDictionary.entryGet(i);
                dictionaryGrid.Rows.Add(en.word, transcription == "en" ? en.transcriptionBritish : en.transcriptionAmerican, en.translation);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            transcription = "en";
            button6.Enabled = false;
            button5.Enabled = true;

            dictionaryGrid.Rows.Clear();
            int count = myDictionary.entryCount();
            for (int i = 0; i < count; i++)
            {
                Dictionary.dictionaryEntry en = myDictionary.entryGet(i);
                dictionaryGrid.Rows.Add(en.word, transcription == "en" ? en.transcriptionBritish : en.transcriptionAmerican, en.translation);
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("Помощник для домашнего чтения v 0.33\nAOL Group:\nСедлярский Михаил\nКузнецов Влад\nПолякова Алёна", "Справка");
        }
    }
}
