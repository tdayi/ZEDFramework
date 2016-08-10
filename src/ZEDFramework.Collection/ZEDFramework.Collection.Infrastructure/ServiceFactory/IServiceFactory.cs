using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.ServiceFactory
{
    public interface IServiceFactory
    {
        TResponse Run<TService, TResponse>(Expression<Func<TService, object>> method) where TResponse : class, new();
        TResponse Run<TService, TResponse>(Expression<Func<TService, object>> method, string endpoint, Binding binding) where TResponse : class, new();
    }
}
