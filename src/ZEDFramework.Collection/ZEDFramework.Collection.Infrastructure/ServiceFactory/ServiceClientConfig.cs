using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ServiceFactory
{
    public class ServiceClientConfig
    {
        public Binding Binding { get; set; }

        public string EndpointAddress { get; set; }

        public Type ServiceType { get; set; }

        public bool IsDynamicEndpoint { get; set; }
    }
}
