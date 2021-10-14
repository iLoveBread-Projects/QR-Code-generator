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
using System.Text.RegularExpressions;
using Image = System.Drawing.Image;

namespace QRCodeGenerator
{
    public partial class frmQRCodeGen : Form
    {
        public static string name;
        public frmQRCodeGen()
        {
            InitializeComponent();
            this.MaximizeBox = false;
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
                try
                {
                    var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                    var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                    name = r.Replace(txtLink.Text, "");
                    if (name.Length > 20)
                        name = name.Substring(0, 20);
                    var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", txtLink.Text, 180, 180);
                    WebResponse response = default(WebResponse);
                    Stream remoteStream = default(Stream);
                    StreamReader readStream = default(StreamReader);
                    WebRequest request = WebRequest.Create(url);
                    response = request.GetResponse();
                    remoteStream = response.GetResponseStream();
                    readStream = new StreamReader(remoteStream);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                    Console.WriteLine(name);
                    Console.WriteLine(name.Length);
                    img.Save($@".\{name}.png");
                    response.Close();
                    remoteStream.Close();
                    readStream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not save the QR code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            if (picbxCode.Image != null)
            {
                DialogResult question = MessageBox.Show("Are you sure that you want to copy the QR code?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (question == DialogResult.Yes)
                {
                    try
                    {
                        Image img = new Bitmap(picbxCode.Width, picbxCode.Height);

                        Graphics g = Graphics.FromImage(img);

                        g.CopyFromScreen(PointToScreen(picbxCode.Location), new Point(0, 0), new Size(picbxCode.Width, picbxCode.Height));

                        Clipboard.SetImage(img);

                        g.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was an error while trying to copy the QR code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (question == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}