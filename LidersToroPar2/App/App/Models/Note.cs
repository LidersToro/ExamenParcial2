using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Models
{
    class Note
    {
        [Key]
        public int NotesId { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string Contents { get; set; }
    }
}
