using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZEDFramework.Collection.Infrastructure.Mvc
{
    public class SessionHelper
    {
        public static void Add<T>(T value)
        {
            Guid uniqueId = typeof(T).GUID;

            if (HttpContext.Current.Session[uniqueId.ToString()] != null)
            {
                throw new Exception("Duplicate Session Value!");
            }

            HttpContext.Current.Session[uniqueId.ToString()] = value;
        }

        public static void AddOrUpdate<T>(T value)
        {
            Guid uniqueId = typeof(T).GUID;

            HttpContext.Current.Session[uniqueId.ToString()] = value;
        }

        public static void Add<T>(T value, string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                throw new Exception("Duplicate Session Value!");
            }

            HttpContext.Current.Session[key] = value;
        }

        public static void AddOrUpdate<T>(T value, string key)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static T Get<T>()
        {
            Guid uniqueId = typeof(T).GUID;
            T value = default(T);

            if (HttpContext.Current.Session[uniqueId.ToString()] != null)
            {
                value = (T)HttpContext.Current.Session[uniqueId.ToString()];
            }

            return value;
        }

        public static T Get<T>(string key)
        {
            T value = default(T);

            if (HttpContext.Current.Session[key] != null)
            {
                value = (T)HttpContext.Current.Session[key];
            }

            return value;
        }

        public static void Remove<T>()
        {
            Guid uniqueId = typeof(T).GUID;

            if (HttpContext.Current.Session[uniqueId.ToString()] == null)
            {
                throw new Exception("Session Value Not Found!");
            }

            HttpContext.Current.Session[uniqueId.ToString()] = null;
        }

        public static void Remove<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                throw new Exception("Session Value Not Found!");
            }

            HttpContext.Current.Session[key] = null;
        }
    }
}
