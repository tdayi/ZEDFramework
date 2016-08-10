using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnitOfWork
{
    public class DbObject
    {
        public object DbProviderObject { get; set; }
        public DbProviderType DbProviderType { get; set; }
    }

    public enum DbProviderType
    {
        EntityFramework = 1,
        NHibernate = 2
    }
}
