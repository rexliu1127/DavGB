using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using aspxtemplate;
public partial class registration_code : ParsedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string checkCode = "";
        checkCode = CreateRandomCode(4);
        Session["RegisterCheckCode"] = checkCode;
        CreateImage(checkCode);
    }
    private string CreateRandomCode(int CodeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9";
        string[] allCharArray = allChar.Split(Convert.ToChar(","));
        string RandomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < CodeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
            }

            int t = rand.Next(10);

            while (temp == t)
            {
                t = rand.Next(10);
            }


            temp = t;
            RandomCode += allCharArray[t];
        }

        return RandomCode;
    }
    private void CreateImage(string checkCode)
    {
        int iwidth = (int)(checkCode.Length * 10);
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 22);
        Graphics g = Graphics.FromImage(image);
        Font f = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
        Brush b = new System.Drawing.SolidBrush(Color.White);
        g.Clear(Color.Black);
        g.DrawString(checkCode, f, b, 2, 2);

        Pen blackPen = new Pen(Color.Black, 0);
        Random random = new Random();
        // Rnd Line
        //for (int i = 0; i < 2; i++)
        //{
        //    int y = random.Next(image.Height);
        //    g.DrawLine(blackPen, 0, y, image.Width, y);
        //}

        //Rnd Curve
        //for (int i = 0; i < 3; i++)
        //{
        //    int x1 = random.Next(image.Width);
        //    int x2 = random.Next(image.Width);
        //    int y1 = random.Next(image.Height);
        //    int y2 = random.Next(image.Height);

        //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        //}

        //Rnd Point
        //for (int i = 0; i < 50; i++)
        //{
        //    int x = random.Next(image.Width);
        //    int y = random.Next(image.Height);

        //    image.SetPixel(x, y, Color.FromArgb(random.Next()));
        //} 

        g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ClearContent();
        Response.ContentType = "image/Jpeg";

        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        image.Dispose();


    }
}
