using BikeSharing.Clients.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Services
{
    public interface ILocationProvider
    {
        Task<ILocationResponse> GetPositionAsync();
    }
}
