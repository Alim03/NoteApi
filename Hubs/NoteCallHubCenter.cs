using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    public class NoteCallHubCenter:Hub<INoteCallCenterHub>
    {
        public async Task NoteCreate(Note note)
        {
            await Clients.All.NoteCreate(note);
        }
        public async Task NoteUpdate(Note note)
        {
            await Clients.All.NoteUpdate(note);
        }
        public async Task NoteDelete(int id)
        {
            await Clients.All.NoteDelete(id);
        }
    }
}