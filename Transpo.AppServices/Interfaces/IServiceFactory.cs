using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.Interfaces
{
    public interface IServiceFactory
    {
        RideService getRideService();

        UserService getUserService();

        CarService getCarService();
    }
}
