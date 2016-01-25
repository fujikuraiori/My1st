using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace aquariusServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private Thread work;
        private TcpListener server;

        private void button1_Click(object sender, EventArgs e)
        {
            work = new Thread(DoWork);
            work.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.Stop();
        }

        private void DoWork(){
            server = new TcpListener(IPAddress.Parse("127.0.0.1"),2345);
            server.Start();
            textBox1.Text = "server start";
            try{
                while(true){
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                     byte[] data = new byte[100];
                    int len = stream.Read(data,0,data.Length);
                    string str = System.Text.Encoding.ASCII.GetString(data,0,len);
                    textBox1.Text = "GetData:"+str;
                    client.Close();
                }
            }
            catch(Exception ex){
                textBox1.Text = "server End";
            }
        }
    }
        
}
