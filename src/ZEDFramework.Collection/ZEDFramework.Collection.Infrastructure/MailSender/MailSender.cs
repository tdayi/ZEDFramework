using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ZEDFramework.Collection.Infrastructure.MailSender
{
    public class MailSender : IMailSender
    {
        public void Send(MailModel mailConfig)
        {
            Action<MailAttachment> action = null;
            using (MailMessage message = new MailMessage())
            {
                SmtpClient client = new SmtpClient
                {
                    Credentials = new NetworkCredential(mailConfig.MailAccountName, mailConfig.MailAccountPassword)
                };
                message.From = new MailAddress(mailConfig.MailAccountName, mailConfig.MailFromName, Encoding.UTF8);
                message.To.Add(mailConfig.MailReceiver);
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = mailConfig.Subject;
                message.Body = mailConfig.Body;
                if (mailConfig.Attachments != null)
                {
                    if (action == null)
                    {
                        action = x => message.Attachments.Add(new Attachment(new MemoryStream(x.File), x.FileName, x.FileType));
                    }
                    mailConfig.Attachments.ForEach(action);
                }
                client.EnableSsl = mailConfig.IsMailSsl;
                client.Port = mailConfig.MailPort;
                client.Host = mailConfig.MailHost;
                client.Send(message);
            }
        }
    }
}
