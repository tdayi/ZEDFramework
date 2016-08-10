using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.UnityFramework.UnityInstaller;

namespace ZEDFramework.Collection.Infrastructure.UnityFramework.Bootstrapper
{
    public class BootstrapperBase
    {
        private readonly string AssemblyName;

        public BootstrapperBase(string assemblyName)
        {
            this.AssemblyName = assemblyName;
            this.Install();
        }

        public void Install()
        {
            IUnityContainer container;
            System.Reflection.AssemblyName assemblyRef = new System.Reflection.AssemblyName
            {
                Name = this.AssemblyName
            };

            List<IUnityInstaller> list = (from y in Assembly.Load(assemblyRef).GetTypes()
                                          where y.IsClass && (y.GetInterface(typeof(IUnityInstaller).Name) != null)
                                          select Activator.CreateInstance(y) as IUnityInstaller).ToList<IUnityInstaller>();

            if (list != null && list.Count != 0)
            {
                container = new UnityContainer();
                container.AddNewExtension<Interception>();
                (from x in list orderby x.Order select x).ToList<IUnityInstaller>().ForEach(x => x.Install(container));
            }
        }
    }
}
