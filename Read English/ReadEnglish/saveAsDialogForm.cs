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
using System.IO;
using System.IO.Compression;

namespace ReadEnglish
{
    public partial class saveAsDialogForm : MetroForm
    {
        MainForm parentForm;
        public saveAsDialogForm()
        {
            InitializeComponent();
        }        
        public saveAsDialogForm(MainForm f)
        {
            InitializeComponent();
            parentForm = f;
            numericUpDown1.Value = parentForm.myMonitor.textRate;
        }

        string fileName = "";
        string filePath = "";
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            fileName = Path.GetFileName(saveFileDialog1.FileName);
            filePath = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            textBox1.Text = saveFileDialog1.FileName;
        }

        private void saveFile(object sender, EventArgs e)
        {
            Directory.CreateDirectory(filePath);

            parentForm.myDictionary.save(filePath + "\\" + "dictionary.txt");
            parentForm.redactorRichTextBox.SaveFile(filePath + "\\" + "text.rtf");
            //parentForm.myMonitor.save(filePath + "\\" + "monitor.txt", (int) numericUpDown1.Value, (int)(numericUpDown1.Value / 200));

            ZipFile.CreateFromDirectory(filePath, saveFileDialog1.FileName, CompressionLevel.NoCompression, false);
            Directory.Delete(filePath, true);
            
            parentForm.myMonitor.textRate = (int) numericUpDown1.Value;
            parentForm.myMonitor.wordsRate = (int)(numericUpDown1.Value / 200);

            this.Close();
        }



        private void saveAsDialogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
