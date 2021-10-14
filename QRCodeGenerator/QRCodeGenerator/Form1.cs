using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;

namespace QRCodeGenerator
{
    public partial class frmQRCodeGen : Form
    {
        public frmQRCodeGen()
        {
            InitializeComponent();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLink.Text != "")
                {
                    var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", txtLink.Text, 180, 180);
                    picbxCode.ImageLocation = url;

                    lblInfo.Text = "Click the QR code to copy it";
                }
                else
                {
                    MessageBox.Show("Please give a link", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not create the QR code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLink.Text != "")
            {
                string name = txtLink.Text.Replace("/", "");
                var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", txtLink.Text, 180, 180);
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                img.Save($"{name}.png");
                response.Close();
                remoteStream.Close();
                readStream.Close();
            }
            else
            {
                MessageBox.Show("Could not save the QR code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtLink.Text = string.Empty;
                lblInfo.Text = string.Empty;
                picbxCode.Image = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not clear the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picbxCode_MouseDoubleClick(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Do you want to copy the QR code to your clipboard?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (question == DialogResult.Yes)
            {
                Image img = new Bitmap(picbxCode.Width, picbxCode.Height);

                Graphics g = Graphics.FromImage(img);

                g.CopyFromScreen(PointToScreen(picbxCode.Location), new Point(0, 0), new Size(picbxCode.Width, picbxCode.Height));

                Clipboard.SetImage(img);

                g.Dispose();
            }
            else if (question == DialogResult.No)
            {
                return;
            }
        }
    }
}