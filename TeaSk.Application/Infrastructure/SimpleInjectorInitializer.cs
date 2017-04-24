using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaSk.Application.Infrastructure
{
    public static class SimpleInjectorInitializer
    {
        public static Container Injectorcontainer { get; set; }

        public static void Initialize()
        {
            var container = new Container();
            var scopedLifetime = Lifestyle.Singleton;
            DependencyContainerMapper.InitializeContainer(container, scopedLifetime);
            Injectorcontainer = container;
            container.Verify();
        }
    }
}
