using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.User;
using Challenge.Models;

namespace Challenge.Hubs
{
    public interface ICallCenterHub
    {
        Task UserCreate(UserDto user);
        Task UserUpdate(UserDto user);
        Task UserDelete(int id);
    }
}