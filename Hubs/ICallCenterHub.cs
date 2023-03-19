using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Hubs
{
    public interface ICallCenterHub
    {
        Task UserCreate(User user);
        Task UserUpdate(User user);
        Task UserDelete(int id);
    }
}