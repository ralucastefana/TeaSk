using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using TeaSk.Data.Infrastructures;

namespace TeaSk.Application.Infrastructure
{
    public class DependencyContainerMapper
    {
        public static void InitializeContainer(Container container, Lifestyle lifeStyle)
        {
            //database      
            container.Register<IDatabaseFactory, DatabaseFactory>(lifeStyle);
            container.Register<IUnitOfWork, UnitOfWork>(lifeStyle);

            //register generic repository
            container.Register(typeof(IRepository<>), typeof(Repository<>), lifeStyle);
            container.Register(typeof(IService<>), typeof(Service<>), lifeStyle);

        }
    }
}
