using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework.UnityWcfHost
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        public IUnityContainer Container { get; set; }

        public Type ServiceType { get; set; }

        public UnityInstanceProvider(Type type, IUnityContainer container)
        {
            this.ServiceType = type;
            this.Container = container;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return UnityContainerExtensions.Resolve(this.Container, this.ServiceType, new ResolverOverride[0]);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            this.Container.Teardown(instance);
        }
    }
}
