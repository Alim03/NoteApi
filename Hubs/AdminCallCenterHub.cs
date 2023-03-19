using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    public class AdminCallCenterHub : Hub<IAdminCallCenterHub>
    {
        public async Task MessageAdded(string message)
        {
            await Clients.All.MessageAdded(message);
        }
        public async Task QueueClear()
        {
            await Clients.All.QueueClear();
        }
    }
}