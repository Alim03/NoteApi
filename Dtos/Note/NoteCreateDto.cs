using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Dtos.Note
{
    public class NoteCreateDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public bool Published { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}