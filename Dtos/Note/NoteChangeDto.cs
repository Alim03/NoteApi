using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Dtos.Note
{
    public class NoteChangeDto : BaseDto
    {
        public string Content { get; set; }
        public DateTime DateModified { get; set; }
        public bool Published { get; set; }
    }
}
