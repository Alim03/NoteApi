using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Hubs
{
    public interface IAdminCallCenterHub
    {
        Task MessageAdded(string message);
        Task QueueClear();
    }
}