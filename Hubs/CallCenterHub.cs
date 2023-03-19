using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    [EnableCors("AllowAll")]
    public class CallCenterHub : Hub<ICallCenterHub>
    {
        public async Task UserCreate(User user)
        {
            await Clients.All.UserCreate(user);
        }
        public async Task UserUpdate(User user)
        {
            await Clients.All.UserUpdate(user);
        }
        public async Task UserDelete(int id)
        {
            await Clients.All.UserDelete(id);
        }
    }
}