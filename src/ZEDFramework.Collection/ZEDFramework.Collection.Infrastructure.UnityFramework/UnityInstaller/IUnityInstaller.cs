using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework.UnityInstaller
{
    public interface IUnityInstaller
    {
        int Order { get; set; }
        void Install(IUnityContainer container);
    }
}
