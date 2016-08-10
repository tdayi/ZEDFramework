using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ServiceFactory
{
    public class ServiceClientConfigContainer
    {
        public static ICollection<ServiceClientConfig> ServiceClientConfigurations { get; set; }
    }
}
