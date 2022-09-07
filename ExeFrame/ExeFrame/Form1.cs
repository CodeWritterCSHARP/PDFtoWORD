using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExeFrame
{
    public partial class Form1 : Form
    {
        private string fileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Word.docx|*.docx| PDF.pdf|*.pdf" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    textBox1.Text = openFileDialog.FileName;
            }
            fileName = textBox1.Text;
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            if (fileName.EndsWith(".docx"))
            {
                fileName = fileName.Remove(fileName.Length - 5);
                process.StandardInput.WriteLine($"move {textBox1.Text} {fileName}.pdf");
                process.StandardInput.Flush();
                process.StandardInput.Close();
            }

            if (fileName.EndsWith(".pdf"))
            {
                fileName = fileName.Remove(fileName.Length - 4);
                process.StandardInput.WriteLine($"move {textBox1.Text} {fileName}.docx");
                process.StandardInput.Flush();
                process.StandardInput.Close();
            }
        }
    }
}
