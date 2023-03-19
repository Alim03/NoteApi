using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Challenge.Dtos.Note;
using Challenge.Models;

namespace Challenge.Dtos.User
{
    public class UserDto:BaseDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string? Website { get; set; }
        public ICollection<NoteDto> Notes { get; set; }
    }
}