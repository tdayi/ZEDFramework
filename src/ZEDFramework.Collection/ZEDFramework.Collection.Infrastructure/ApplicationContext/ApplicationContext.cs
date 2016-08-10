using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZEDFramework.Collection.Infrastructure
{
    public class ApplicationContext
    {
        public ApplicationContext()
        {

        }

        public static bool IsWeb
        {
            get
            {
                return (HttpContext.Current != null);
            }
        }

        public static bool IsWcf
        {
            get
            {
                return (OperationContext.Current != null);
            }
        }

        public static bool IsConsole
        {
            get
            {
                return (OperationContext.Current == null && HttpContext.Current == null);
            }
        }

        public static T Get<T>()
        {
            T data = default(T);

            string key = typeof(T).GUID.ToString();

            if (IsWeb)
            {
                data = WebApplicationContext.Get<T>();
            }
            else if (IsWcf)
            {
                data = WcfApplicationContext.Get<T>();
            }
            else
            {

            }

            return data;
        }

        public static T Get<T>(string key)
        {
            T data = default(T);

            if (IsWeb)
            {
                data = WebApplicationContext.Get<T>(key);
            }
            else if (IsWcf)
            {
                data = WcfApplicationContext.Get<T>(key);
            }
            else
            {

            }

            return data;
        }

        public static void Set<T>(T value)
        {
            if (IsWcf)
            {
                WcfApplicationContext.Set<T>(value);
            }
            else if (IsWeb)
            {
                WebApplicationContext.Set<T>(value);
            }
            else
            {

            }
        }

        public static void Set<T>(T value, string key)
        {
            if (IsWcf)
            {
                WcfApplicationContext.Set<T>(value, key);
            }
            else if (IsWeb)
            {
                WebApplicationContext.Set<T>(value, key);
            }
            else
            {

            }
        }
    }
}

