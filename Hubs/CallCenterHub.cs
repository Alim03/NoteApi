using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    public class CallCenterHub : Hub<ICallCenterHub>
    {
        public async Task UserChangeReceived(User user)
        {
            await Clients.All.UserChangeReceived(user);
        }
    }
}