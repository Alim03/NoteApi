using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.User;
using Challenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    [EnableCors("AllowAll")]
    public class CallCenterHub : Hub<ICallCenterHub>
    {
        public async Task UserCreate(UserDto user)
        {
            await Clients.All.UserCreate(user);
        }
        public async Task UserUpdate(UserDto user)
        {
            await Clients.All.UserUpdate(user);
        }
        public async Task UserDelete(int id)
        {
            await Clients.All.UserDelete(id);
        }
    }
}