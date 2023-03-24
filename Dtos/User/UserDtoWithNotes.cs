using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.Note;

namespace Challenge.Dtos.User
{
     public class UserDtoWithNotes :BaseDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string? Website { get; set; }
        public ICollection<NoteDto> Notes { get; set; }
    }
}