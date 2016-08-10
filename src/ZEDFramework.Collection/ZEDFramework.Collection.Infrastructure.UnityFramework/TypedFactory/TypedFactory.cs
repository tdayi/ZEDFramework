using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.ServiceFactory;
using ZEDFramework.Collection.Infrastructure.TypedFactory;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework
{
    public class TypedFactory : ITypedFactory
    {
        private readonly IUnityContainer UnityContainer;

        public TypedFactory(
            IUnityContainer unityContainer)
        {
            this.UnityContainer = unityContainer;
        }

        public T Resolve<T>()
        {
            return this.UnityContainer.Resolve<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this.UnityContainer.ResolveAll<T>();
        }
    }
}
