using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.UnitOfWork;

namespace ZEDFramework.Collection.Infrastructure.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext Context;
        private DbContextTransaction ContextTransaction;
        private UnitOfWorkBehavior UnitOfWorkBehavior;
        private static object LockObject = new object();
        private static ConcurrentDictionary<string, object> UowItems = new ConcurrentDictionary<string, object>();
        private bool UseWithoutTransaction;

        public UnitOfWork(IDbObjectFactory dbObjectFactory)
        {
            this.Initialize(dbObjectFactory, new UnitOfWorkBehavior());
        }

        public UnitOfWork(IDbObjectFactory dbObjectFactory, UnitOfWorkBehavior behavior)
        {
            this.Initialize(dbObjectFactory, behavior);
        }

        public void Begin(bool useWithoutTransaction = false)
        {
            try
            {
                lock (LockObject)
                {
                    this.UseWithoutTransaction = useWithoutTransaction;
                    if (!useWithoutTransaction)
                    {
                        this.ContextTransaction = this.Context.Database.BeginTransaction(this.UnitOfWorkBehavior.IsolationLevel);
                    }
                    this.AddDbObject();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Commit()
        {
            try
            {
                lock (LockObject)
                {
                    UnitOfWork unitOfWork = GetUnitOfWork(false);
                    unitOfWork.Context.SaveChanges();
                    if (!unitOfWork.UseWithoutTransaction)
                    {
                        unitOfWork.ContextTransaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                lock (LockObject)
                {
                    UnitOfWork unitOfWork = GetUnitOfWork(true);
                    unitOfWork.Context.Dispose();
                    if (!this.UseWithoutTransaction)
                    {
                        unitOfWork.ContextTransaction.Dispose();
                    }
                    GC.SuppressFinalize(unitOfWork);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void AddDbObject()
        {
            try
            {
                lock (LockObject)
                {
                    if (string.IsNullOrWhiteSpace(Thread.CurrentThread.Name))
                    {
                        Thread.CurrentThread.Name = this.UnitOfWorkBehavior.FactoryKey;
                    }

                    Stack<UnitOfWork> stack = null;

                    if (ApplicationContext.IsConsole)
                    {
                        stack = UowItems.ContainsKey(Thread.CurrentThread.Name)
                            ? (Stack<UnitOfWork>)UowItems[Thread.CurrentThread.Name]
                            : null;
                    }
                    else
                    {
                        stack = ApplicationContext.Get<Stack<UnitOfWork>>(Thread.CurrentThread.Name) != null
                            ? ApplicationContext.Get<Stack<UnitOfWork>>(Thread.CurrentThread.Name)
                            : null;
                    }

                    if (stack == null)
                    {
                        stack = new Stack<UnitOfWork>();

                        if (ApplicationContext.IsConsole)
                        {
                            UowItems[Thread.CurrentThread.Name] = stack;
                        }
                        else
                        {
                            ApplicationContext.Set<Stack<UnitOfWork>>(stack, Thread.CurrentThread.Name);
                        }
                    }

                    stack.Push(this);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static object GetDbObject()
        {
            return GetUnitOfWork(false).Context;
        }

        internal static object GetDbObject(string factoryKey)
        {
            return GetUnitOfWork(factoryKey, false).Context;
        }

        internal static UnitOfWork GetUnitOfWork(bool dispose = false)
        {
            return GetUnitOfWork(null, dispose);
        }

        internal static UnitOfWork GetUnitOfWork(string factoryKey, bool dispose = false)
        {
            UnitOfWork uowork = null;

            try
            {
                lock (LockObject)
                {
                    string key = Thread.CurrentThread.Name;

                    if (!string.IsNullOrWhiteSpace(factoryKey))
                    {
                        key = factoryKey;
                    }

                    Stack<UnitOfWork> stack = null;

                    if (ApplicationContext.IsConsole)
                    {
                        stack = (Stack<UnitOfWork>)UowItems[key];
                    }
                    else
                    {
                        stack = ApplicationContext.Get<Stack<UnitOfWork>>(Thread.CurrentThread.Name);
                    }

                    uowork = dispose ? stack.Pop() : stack.Peek();

                    if (ApplicationContext.IsConsole && stack.Count == 0)
                    {
                        DeleteUnitOfWorkItem(key);
                    }

                    if (uowork == null)
                    {
                        throw new ObjectDisposedException("Stack UnitOfWork item not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return uowork;
        }

        private static void DeleteUnitOfWorkItem(string key)
        {
            try
            {
                object stackObj;
                UowItems.TryRemove(key, out stackObj);
                stackObj = null;
            }
            catch
            {
                throw;
            }
        }

        private void Initialize(IDbObjectFactory dbObjectFactory, UnitOfWorkBehavior behavior)
        {
            try
            {
                this.UnitOfWorkBehavior = behavior;
                DbObject dbObject = null;

                if (String.IsNullOrEmpty(this.UnitOfWorkBehavior.ConnectionString))
                {
                    dbObject = dbObjectFactory.CreateInstance(this.UnitOfWorkBehavior.EntityLazyLoad);
                }
                else
                {
                    dbObject = dbObjectFactory.CreateInstance(this.UnitOfWorkBehavior.EntityLazyLoad, this.UnitOfWorkBehavior.ConnectionString);
                }

                if (dbObject == null)
                {
                    throw new ArgumentNullException("DbObject IsRequired!");
                }

                if (dbObject.DbProviderType != DbProviderType.EntityFramework)
                {
                    throw new ArgumentException("DbProvider Error. This is UnitOfWork for EntityFramework executing.");
                }

                this.Context = (DbContext)dbObject.DbProviderObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
