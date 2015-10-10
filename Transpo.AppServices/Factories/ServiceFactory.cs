using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.Interfaces;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Repositories;

namespace Transpo.AppServices.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        public TranspoDbContext DbContext;

        public ServiceFactory()
        {
            DbContext = new TranspoDbContext();
        }

        public RideService getRideService()
        {
            return new RideService(new RideRepository(DbContext), new UserRepository(DbContext), new CriticalPointRepository(DbContext));
        }

        public UserService getUserService()
        {
            return new UserService(new UserRepository(DbContext));
        }

        public CarService getCarService()
        {
            return new CarService(new CarRepository(DbContext));
        }
    }
}
