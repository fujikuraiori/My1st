using System;
using System.Windows.Forms;
using System.Net.Sockets;

namespace aquariusChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            try {
                client.Connect("localhost",2345);
                NetworkStream stream = client.GetStream();
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes("GET /start.htm HTTP/1.0\r\n\r\n");
                stream.Write(buffer,0,buffer.Length);
                textBox1.Text = "正常に送信できました";
                client.Close();
            }
            catch(Exception ex){
                textBox1.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(textBox1.Text, 2345);
                NetworkStream stream = client.GetStream();
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(textBox2.Text);
                stream.Write(buffer, 0, buffer.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }
        }
    }
}
