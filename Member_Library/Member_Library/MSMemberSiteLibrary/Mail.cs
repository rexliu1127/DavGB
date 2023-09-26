using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

/// <summary>
/// Mail 的摘要描述
/// </summary>
namespace member_site
{
    public class Mail
    {
        private SmtpClient xSender = new SmtpClient();
        private MailMessage xMsg =new MailMessage();
        public String MailFrom="";
        public String[] MailTo;
        public String Subject="";
        public String Body="";
        public string Host="";

        public Mail()
        {
         
        }

        public Mail(string nHost)
        {
            Host = nHost;
        }

        public void AddAttachmentFile(string nFilePath){
            try{
                Attachment attachmentFile = new Attachment(nFilePath);
                xMsg.Attachments.Add(attachmentFile);
            }
            catch(Exception ex){
                throw ex;
           }
        }

        public void AddImageLink(string nContentId,string nImgRealPath)
        {
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<img src=cid:" + nContentId + ">", null, "text/html");
            LinkedResource img = new LinkedResource(nImgRealPath);
            img.ContentId = nContentId;
            htmlView.LinkedResources.Add(img);
            xMsg.AlternateViews.Add(htmlView);
        }

        public void SendAsync()
        {

            try{
                 //Send Mail
                xMsg.From = new MailAddress(MailFrom);
                for (int i = 0; i < MailTo.Length ; i++)
                {
                     xMsg.To.Add(new MailAddress(MailTo[i]));
                }
               
                xMsg.Subject = Subject;
                xMsg.Body = Body;
                xMsg.IsBodyHtml = true;
             //   xMsg.BodyEncoding = System.Text.Encoding.UTF8;
                xSender.Host  = Host;
                //xSender.Timeout = 60;

                xSender.SendAsync(xMsg, "XXX Error Message Report!!");
            }
            catch (Exception  ex){
                throw ex;
            }
        }

        public void Send()
        {

            try
            {
                //Send Mail
                xMsg.From = new MailAddress(MailFrom);
                for (int i = 0; i < MailTo.Length; i++)
                {
                    xMsg.To.Add(new MailAddress(MailTo[i]));
                }

                xMsg.Subject = Subject;
                xMsg.Body = Body;
                xMsg.IsBodyHtml = true;
                //   xMsg.BodyEncoding = System.Text.Encoding.UTF8;
                xSender.Host = Host;
                //xSender.Timeout = 60;

                xSender.Send (xMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
