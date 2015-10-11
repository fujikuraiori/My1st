using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace unit20151011
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void newfile_Click_1(object sender, EventArgs e)
        {
            //textfileを作成する
            string fName = textBox1.Text;
            string text = TXTBOX.Text;
            StreamWriter textFile;
            textFile = new StreamWriter(@"./"+fName+".txt", false, System.Text.Encoding.Default);
            textFile.WriteLine(text);
            textFile.Close();
        }

        private void readfile_Click_1(object sender, EventArgs e)
        {
            string fName = textBox1.Text;
            if (File.Exists("./" + fName + ".txt") == false)
            {
                TXTBOX.Text = "ファイルが存在しません";
                return;
            }
            StreamReader textFile = new StreamReader(@"./" + fName + ".txt", System.Text.Encoding.Default);
            TXTBOX.Text = textFile.ReadToEnd();
            textFile.Close();
        }
    }
}