using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.Note;
using Challenge.Models;

namespace Challenge.Hubs
{
    public interface INoteCallCenterHub
    {
        Task NoteCreate(NoteDto note);
        Task NoteUpdate(NoteChangeDto note);
        Task NoteDelete(int id);
        Task NoteViewsChange(int views);
    }
}
