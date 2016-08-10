using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.MailSender
{
    public interface IMailSender
    {
        void Send(MailModel model);
    }
}
