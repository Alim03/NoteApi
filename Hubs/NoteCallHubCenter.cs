using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.Note;
using Challenge.Models;
using Microsoft.AspNetCore.SignalR;

namespace Challenge.Hubs
{
    public class NoteCallHubCenter : Hub<INoteCallCenterHub>
    {
        public async Task NoteCreate(NoteDto note)
        {
            await Clients.All.NoteCreate(note);
        }

        public async Task NoteUpdate(NoteChangeDto note)
        {
            await Clients.All.NoteUpdate(note);
        }

        public async Task NoteDelete(int id)
        {
            await Clients.All.NoteDelete(id);
        }

        public async Task NoteViewsChange(int views)
        {
            await Clients.All.NoteDelete(views);
        }
    }
}
