using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZEDFramework.Collection.Infrastructure.Mvc
{
    public static class ResultHelper
    {
        public static JsonResult Json(object obj)
        {
            return new JsonResult
            {
                Data = obj,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = 0x7fffffff
            };
        }
    }
}
