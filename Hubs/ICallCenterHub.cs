using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Hubs
{
    public interface ICallCenterHub
    {
        Task UserChangeReceived(User user);
    }
}