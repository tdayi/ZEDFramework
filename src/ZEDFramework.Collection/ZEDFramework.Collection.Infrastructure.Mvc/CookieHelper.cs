using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZEDFramework.Collection.Infrastructure.Mvc
{
    public class CookieHelper
    {
        public static void Set(
            string name,
            string value,
            DateTime expireDate)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expireDate;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string Get(
            string name)
        {
            string cookieValue = String.Empty;

            if (HttpContext.Current == null)
            {
                return cookieValue;
            }

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            if (cookie != null)
            {
                return cookieValue = cookie.Value;
            }

            return cookieValue;
        }

        public static void Kill(
            string name)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];

            if (cookie != null)
            {
                TimeSpan timeSpan = cookie.Expires - DateTime.Now;

                Set(name, null, DateTime.Now.AddDays(-timeSpan.Days));
            }
        }
    }
}
