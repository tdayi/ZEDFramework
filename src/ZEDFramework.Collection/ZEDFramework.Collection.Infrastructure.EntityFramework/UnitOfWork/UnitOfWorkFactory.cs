using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.UnitOfWork;

namespace ZEDFramework.Collection.Infrastructure.EntityFramework.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IDbObjectFactory DbObjectFactory;

        public UnitOfWorkFactory(IDbObjectFactory dbObjectFactory)
        {
            this.DbObjectFactory = dbObjectFactory;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(this.DbObjectFactory);
        }

        public IUnitOfWork Create(UnitOfWorkBehavior behavior)
        {
            return new UnitOfWork(this.DbObjectFactory, behavior);
        }

        public IUnitOfWork GetCurrent()
        {
            return UnitOfWork.GetUnitOfWork(false);
        }

        public IUnitOfWork GetCurrent(string factoryKey)
        {
            return UnitOfWork.GetUnitOfWork(factoryKey, false);
        }

        public object GetCurrentDbObject
        {
            get
            {
                return UnitOfWork.GetDbObject(null);
            }
        }
    }
}
