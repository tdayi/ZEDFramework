using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZEDFramework.Collection.Infrastructure
{
    internal static class WebApplicationContext
    {
        public static T Get<T>()
        {
            T local = default(T);
            string key = typeof(T).GUID.ToString();
            if (HttpContext.Current.Items.Contains(key))
            {
                local = (T)HttpContext.Current.Items[key];
            }
            return local;
        }

        public static T Get<T>(string key)
        {
            T local = default(T);
            if (HttpContext.Current.Items.Contains(key))
            {
                local = (T)HttpContext.Current.Items[key];
            }
            return local;
        }

        public static void Set<T>(T value)
        {
            string str = typeof(T).GUID.ToString();
            HttpContext.Current.Items[str] = value;
        }

        public static void Set<T>(T value, string key)
        {
            HttpContext.Current.Items[key] = value;
        }
    }
}
