using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.MailSender
{
    public class MailModel
    {
        public List<MailAttachment> Attachments { get; set; }

        public string Body { get; set; }

        public bool IsMailSsl { get; set; }

        public string MailAccountName { get; set; }

        public string MailAccountPassword { get; set; }

        public string MailFromName { get; set; }

        public string MailHost { get; set; }

        public int MailPort { get; set; }

        public string MailReceiver { get; set; }

        public string Subject { get; set; }
    }

    public class MailAttachment
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }
    }
}
