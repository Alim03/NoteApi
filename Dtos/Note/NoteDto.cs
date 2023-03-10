using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Dtos.Note
{
    public class NoteDto : BaseDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public bool Published { get; set; }
    }
}