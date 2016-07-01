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

        public ServiceFactory(string connectionString)
        {
            DbContext = new TranspoDbContext(connectionString);
        }

        public RideService GetRideService()
        {
            return new RideService(new RideRepository(DbContext), new UserRepository(DbContext),
                new CriticalPointRepository(DbContext), new OrderedCriticalPointRepository(DbContext));
        }

        public CarService GetCarService()
        {
            return new CarService(new CarRepository(DbContext));
        }

        public UserService GetUserService()
        {
            return new UserService(new UserRepository(DbContext));
        }

        public MessageService GetMessageService()
        {
            return new MessageService(new MessageRepository(DbContext), new UserRepository(DbContext));
        }
    }
}
