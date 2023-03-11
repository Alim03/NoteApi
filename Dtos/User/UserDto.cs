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
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [Range(0,128)]
        public int Age { get; set; }
        public string? Website { get; set; }
        public ICollection<NoteDto> Notes { get; set; }
    }
}