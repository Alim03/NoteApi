using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Hubs
{
    public interface INoteCallCenterHub
    {
        Task NoteCreate(Note note);
        Task NoteUpdate(Note note);
        Task NoteDelete(int id);
    }
}