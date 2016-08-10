using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework.UnityWcfHost
{
    public class UnityServiceHost : ServiceHost
    {
        public IUnityContainer Container { get; set; }

        public IDispatchMessageInspector DispatchMessageInspector { get; set; }

        public UnityServiceHost()
        {
            this.Container = new UnityContainer();
        }

        public UnityServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        protected override void OnOpening()
        {
            if (base.Description.Behaviors.Find<UnityServiceBehavior>() == null)
            {
                base.Description.Behaviors.Add(new UnityServiceBehavior(this.Container, this.DispatchMessageInspector));
            }

            base.OnOpening();
        }
    }
}
