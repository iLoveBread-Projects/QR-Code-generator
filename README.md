# QR-Code-generator

### *What is it?*
> It is a open source generator which creates a QR code for the given link or text. You can copy, save or just generate a QR code of choice. **The code is written in C#** and uses a **Google API**

### *How to install*
> If you want to install the full project, then download the full repository.
> 
> If you want to download the program only, [click here](https://github.com/error404-69-dotcom/QR-Code-generator/raw/main/QRCodeGenerator/bin/Release/QRCodeGenerator.exe)

### *How to use the generator*
1. Input a link in the textbox.
2. * Click on *Generate* to show it in the application.
   * Click on *Save* to the directory the .exe file is located in.
3. If you clicked on *Generate* you will be able to copy the QR code to your clipboard by **clicking on the image**.

### *Explaining the code*
> The main code is in the [Form1.cs](https://github.com/error404-69-dotcom/QR-Code-generator/blob/main/QRCodeGenerator/Form1.cs) file. You can follow along there if you want to know how to generator was made.


#### Enjoy the QR code generator and let me know if you think it is useful or not
<details closed>
<summary* The event when the *Generate* button is clicked.</summary>
<br>
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
