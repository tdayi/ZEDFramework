using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ZEDFramework.Collection.Infrastructure
{
    internal static class WcfApplicationContext
    {
        public static T Get<T>()
        {
            T local = default(T);
            string key = typeof(T).GUID.ToString();
            if (WcfOperationContext.Current.Items.ContainsKey(key))
            {
                local = (T)WcfOperationContext.Current.Items[key];
            }
            return local;
        }

        public static T Get<T>(string key)
        {
            T local = default(T);
            if (WcfOperationContext.Current.Items.ContainsKey(key))
            {
                local = (T)WcfOperationContext.Current.Items[key];
            }
            return local;
        }

        public static void Set<T>(T value)
        {
            string str = typeof(T).GUID.ToString();
            WcfOperationContext.Current.Items[str] = value;
        }

        public static void Set<T>(T value, string key)
        {
            WcfOperationContext.Current.Items[key] = value;
        }
    }

    internal class WcfOperationContext : IExtension<OperationContext>
    {
        private readonly IDictionary<string, object> ContextItems = new Dictionary<string, object>();

        private WcfOperationContext()
        {
        }

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }

        public static WcfOperationContext Current
        {
            get
            {
                WcfOperationContext item = OperationContext.Current.Extensions.Find<WcfOperationContext>();
                if (item == null)
                {
                    item = new WcfOperationContext();
                    OperationContext.Current.Extensions.Add(item);
                }
                return item;
            }
        }

        public IDictionary<string, object> Items
        {
            get
            {
                return this.ContextItems;
            }
        }
    }
}
