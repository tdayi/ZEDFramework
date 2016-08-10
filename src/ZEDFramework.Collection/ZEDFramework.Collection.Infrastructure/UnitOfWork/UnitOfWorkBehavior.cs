using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnitOfWork
{
    public class UnitOfWorkBehavior
    {
        public UnitOfWorkBehavior()
        {
            this.FactoryKey = Guid.NewGuid().ToString();
            this.EntityLazyLoad = false;
            this.ConnectionString = String.Empty;
            this.IsolationLevel = IsolationLevel.ReadCommitted;
        }

        public string FactoryKey { get; set; }
        public bool EntityLazyLoad { get; set; }
        public string ConnectionString { get; set; }
        public IsolationLevel IsolationLevel { get; set; }
    }
}
