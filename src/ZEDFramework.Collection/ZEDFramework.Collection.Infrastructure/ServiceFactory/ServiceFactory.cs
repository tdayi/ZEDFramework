using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ServiceFactory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceFactoryListener ServiceFactoryListener;
        private readonly IDynamicEndpointBehavior DynamicEndpointBehavior;

        public ServiceFactory()
        {
            this.ServiceFactoryListener = null;
            this.DynamicEndpointBehavior = null;
        }

        public ServiceFactory(
            IServiceFactoryListener serviceFactoryListener,
            IDynamicEndpointBehavior dynamicEndpointBehavior)
        {
            this.ServiceFactoryListener = serviceFactoryListener;
            this.DynamicEndpointBehavior = dynamicEndpointBehavior;
        }

        public TResponse Run<TService, TResponse>(Expression<Func<TService, object>> method) where TResponse : class, new()
        {
            TResponse response = default(TResponse);

            ServiceClientConfig config = (from x in ServiceClientConfigContainer.ServiceClientConfigurations
                                          where x.ServiceType == typeof(TService)
                                          select x).FirstOrDefault<ServiceClientConfig>();
            if (config == null)
            {
                throw new ArgumentNullException("ServiceClientConfig Not Found!");
            }

            string endpointAddress = String.Empty;

            if (config.IsDynamicEndpoint && this.DynamicEndpointBehavior != null)
            {
                endpointAddress = this.DynamicEndpointBehavior.GetEndpointByServiceTypeName(config.ServiceType.Name);
            }
            else
            {
                endpointAddress = config.EndpointAddress;
            }

            ChannelFactory<TService> factory = new ChannelFactory<TService>(config.Binding, new EndpointAddress(endpointAddress));

            TService channel = factory.CreateChannel();

            try
            {
                response = (TResponse)method.Compile()(channel);

                if (this.ServiceFactoryListener != null)
                {
                    this.ServiceFactoryListener.ResultHandling<TResponse>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                factory.Abort();
                factory.Close();
            }

            return response;
        }

        public TResponse Run<TService, TResponse>(Expression<Func<TService, object>> method, string endpoint, System.ServiceModel.Channels.Binding binding) where TResponse : class, new()
        {
            TResponse response = default(TResponse);

            ChannelFactory<TService> factory = new ChannelFactory<TService>(binding, new EndpointAddress(endpoint));

            TService channel = factory.CreateChannel();

            try
            {
                response = (TResponse)method.Compile()(channel);

                if (this.ServiceFactoryListener != null)
                {
                    this.ServiceFactoryListener.ResultHandling<TResponse>(response);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                factory.Abort();
                factory.Close();
            }

            return response;
        }
    }
}
