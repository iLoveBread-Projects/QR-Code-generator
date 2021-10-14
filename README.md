# QR-Code-generator

### *What is it?*
> It is a open source generator which creates a QR code for the given link or text. You can copy, save or just generate a QR code of choice. **The code is written in C#** and uses a **Google API**

### *How to install*
> If you want to install the full project, then download the full repository and open the [QRCodeGenerator.sln](https://github.com/iLoveBread-Code/QR-Code-generator/blob/main/QRCodeGenerator.sln) file in Visual Studio Community.
> 
> If you want to download the application only, [click here](https://github.com/error404-69-dotcom/QR-Code-generator/raw/main/QRCodeGenerator/bin/Release/QRCodeGenerator.exe)

### *How to use the generator*
1. Input a link or text in the textbox.
2. * Click on *Generate* to show it in the application.
   * Click on *Save* to the directory the .exe file is located in.
3. If you clicked on *Generate* you will be able to copy the QR code to your clipboard by **clicking on the image**.
4. If you click on *Clear* you will clear the QR code and the text box from the application, so you can start from scratch again.

### *Explaining the code*
> The main code is in the [Form1.cs](https://github.com/error404-69-dotcom/QR-Code-generator/blob/main/QRCodeGenerator/Form1.cs) file. You can follow along there if you want to know how the generator was made.

<details closed><summary>The event when the Generate button is clicked.</summary>

```csharp
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
```

</details>

<details closed><summary>The event when the Save button is clicked.</summary>
  
```csharp
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
```

</details>

<details><summary>The event when the Clear button is clicked.</summary>

```csharp
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
```
  
</details>

<details><summary>The event when you click the QR code.</summary>

```csharp
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
```
  
</details>

#### *Enjoy the QR code generator*
