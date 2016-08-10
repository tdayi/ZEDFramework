using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework.UnityWcfHost
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        public IUnityContainer Container { get; set; }

        public IDispatchMessageInspector DispatchMessageInspector { get; set; }

        public UnityServiceBehavior(
            IUnityContainer container,
            IDispatchMessageInspector dispatchMessageInspector)
        {
            this.Container = container;
            this.DispatchMessageInspector = dispatchMessageInspector;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (Type serviceType in serviceDescription.ServiceType.GetInterfaces())
            {
                IInstanceProvider provider = new UnityInstanceProvider(serviceType, this.Container);

                foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
                {
                    foreach (EndpointDispatcher endpoint in dispatcher.Endpoints)
                    {
                        endpoint.DispatchRuntime.InstanceProvider = provider;
                        if (this.DispatchMessageInspector != null)
                        {
                            endpoint.DispatchRuntime.MessageInspectors.Add(this.DispatchMessageInspector);
                        }
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
