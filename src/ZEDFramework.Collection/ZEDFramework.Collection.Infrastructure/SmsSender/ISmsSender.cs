﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.SmsSender
{
    public interface ISmsSender
    {
        void Send(SmsModel model);
    }
}
