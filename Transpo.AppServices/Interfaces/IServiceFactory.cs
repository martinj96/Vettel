using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.Interfaces
{
    public interface IServiceFactory
    {
        RideService GetRideService();

        UserService GetUserService();

        CarService GetCarService();

        MessageService GetMessageService();
    }
}
