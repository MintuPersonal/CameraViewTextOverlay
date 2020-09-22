using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ipcameraoverlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SendToHikVision("admin", "admin@123", "10.11.1.16", "80");
        }

        void SendToHikVision(string UserName, string Password, string IP, string Port)
        {
            try
            {
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://10.11.1.16//ISAPI/System/Video/inputs/channels/1/overlays/text");
                wr.Accept = "*/*";
                wr.Method = "PUT";
                wr.ReadWriteTimeout = 5000;
                //wr.UseDefaultCredentials = false;
                wr.Credentials = new NetworkCredential(UserName, Password);

                byte[] pBytes = GetDHCPPost();
                wr.ContentLength = pBytes.Length;

                using (Stream DS = wr.GetRequestStream())
                {
                    DS.Write(pBytes, 0, pBytes.Length);
                    DS.Close();
                }
                wr.BeginGetResponse(r => { var reponse = wr.EndGetResponse(r); }, null);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        byte[] GetDHCPPost()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("<TextOverlayList  version=\"2.0\" xmlns=\"http://www.hikvision.com/ver10/XMLSchema\">");
            sb.AppendLine("<TextOverlay>");
            sb.AppendLine("<id>1</id>");
            sb.AppendLine("<enabled>true</enabled>");
            sb.AppendLine("<positionX>100</positionX>");
            sb.AppendLine("<positionY>500</positionY>");
            sb.AppendLine("<displayText>"+textBox1.Text+" </displayText>");         
            sb.AppendLine("</TextOverlay>");

          //  sb.AppendLine("<TextOverlay>");
            //sb.AppendLine("<id>2</id>");
            //sb.AppendLine("<enabled>true</enabled>");
            //sb.AppendLine("<positionX>100</positionX>");
            //sb.AppendLine("<positionY>480</positionY>");
            //sb.AppendLine("<displayText>product-1 </displayText>");
            //sb.AppendLine("</TextOverlay>");


            //sb.AppendLine("<TextOverlay>");
            //sb.AppendLine("<id>3</id>");
            //sb.AppendLine("<enabled>true</enabled>");
            //sb.AppendLine("<positionX>100</positionX>");
            //sb.AppendLine("<positionY>440</positionY>");
            //sb.AppendLine("<displayText>product-2 </displayText>");
            //sb.AppendLine("</TextOverlay>");


            //sb.AppendLine("<TextOverlay>");
            //sb.AppendLine("<id>4</id>");
            //sb.AppendLine("<enabled>true</enabled>");
            //sb.AppendLine("<positionX>100</positionX>");
            //sb.AppendLine("<positionY>410</positionY>");
            //sb.AppendLine("<displayText>1500 </displayText>");
          //  sb.AppendLine("</TextOverlay>");




            sb.AppendLine("</TextOverlayList> ");


            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
